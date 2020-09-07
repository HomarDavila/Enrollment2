-- =============================================      
-- Author:  <Author,,Name>      
-- Create date: <Create Date,,>      
-- Description: <Description,,>      
-- =============================================      
CREATE PROCEDURE [ExportAses].[USP_UploadDataReject]       
 @Reject_FilePath VARCHAR(500),      
 @Reject_FileName VARCHAR(100),      
 @ProcessType VARCHAR(20),      
 @UserName VARCHAR(30)      
AS      
BEGIN      
 DECLARE @SqlStatement VARCHAR(500)      
 DECLARE @IdInserted INT      
 DECLARE @Procedure VARCHAR(100)      
 DECLARE @Rows int      
 DECLARE @Error VARCHAR(300)      
 DECLARE @Start DATETIME      
 DECLARE @End DATETIME      
 CREATE TABLE #TEMP (Data VARCHAR(2000) COLLATE SQL_Latin1_General_CP1_CI_AI)      
 DECLARE @TransactionName VARCHAR(20) = 'PROCESS_TRANSACTION_REJECT_RECORD'      
      
 BEGIN TRY      
  SET @Procedure = OBJECT_NAME(@@PROCID)      
  SET @Start = GETDATE()      
  SET @SqlStatement = 'BULK INSERT #TEMP FROM ''' + @Reject_FilePath +       
  ''' WITH(ROWTERMINATOR = ''' + CHAR(10) + ''', CODEPAGE = ''' + 'ACP' + ''')' -- DATAFILETYPE = 'WIDECHAR'      
  EXECUTE (@SqlStatement)      
      
  BEGIN TRAN @TransactionName      
  INSERT INTO [ExportAses].[ProcessHeader](      
   FileName,      
   ProcessType,      
   StartTime,      
   EndTime,      
   CreatedBy,      
   CreatedOn,      
   Enabled,      
   Exception      
  ) VALUES(      
   @Reject_FileName,      
   @ProcessType,      
   @Start,      
   NULL,      
   @UserName,      
   CONVERT(DATE, GETDATE()),      
   1,      
   NULL      
  )      
  SET @IdInserted = @@IDENTITY      
  INSERT INTO [ExportAses].[ImportRejectfileFromAses] (      
	[FAMILIY_ID],
	[REGION_ID],
	[PROCESS_DATE],
	[APP_NUM],
	[REF_FILENAMES],
	[REJ_FILENAMES],
	[ERROR_CODE],
	[ERROR_CODE_DESC],
	[CREATE_DATE],
	[RESOLVED_DATE],
	[UPDATE_USER],
	[UPDATE_DATE],
	[FILLER],
	[PROCESS_HEADERID]
  )      
  SELECT       
	SUBSTRING(Data,1,11) [FAMILIY_ID],
	SUBSTRING(Data,12,1) [REGION_ID],
	SUBSTRING(Data,13,8) [PROCESS_DATE],
	SUBSTRING(Data,21,10) [APP_NUM],
	SUBSTRING(Data,31,50) [REF_FILENAMES],
	SUBSTRING(Data,81,50) [REJ_FILENAMES],
	SUBSTRING(Data,131,30) [ERROR_CODE],
	SUBSTRING(Data,161,400) [ERROR_CODE_DESC],
	SUBSTRING(Data,561,8) [CREATE_DATE],
	SUBSTRING(Data,569,8) [RESOLVED_DATE],
	SUBSTRING(Data,577,8) [UPDATE_USER],
	SUBSTRING(Data,585,8) [UPDATE_DATE],
	SUBSTRING(Data,593,1) [FILLER],
	@IdInserted
  FROM #TEMP;      
  SET @Rows = @@ROWCOUNT      
  SET @Error = @@ERROR      
      
  UPDATE [ExportAses].[ProcessHeader]      
  SET EndTime = GETDATE()      
  WHERE ID = @IdInserted      
      
  COMMIT TRAN @TransactionName      
      
  SELECT      
  CAST(@IdInserted as varchar(10)) As ProcessHeaderId      
  ,@ProcessType AS ProcessType      
  ,@Error AS ErrorNumber      
  ,@Procedure AS ProcedureName      
  ,NULL AS ErrorLine      
  ,CAST(@Rows as varchar(10)) + ' Affected Rows' AS ProcessMessage      
 END TRY      
 BEGIN CATCH      
  ROLLBACK TRAN @TransactionName      
  SELECT      
   NULL As ProcessHeaderId      
  ,@ProcessType AS ProcessType      
  ,CAST(ERROR_NUMBER() AS VARCHAR) AS ErrorNumber      
  ,ERROR_PROCEDURE() AS ProcedureName      
  ,CAST(ERROR_LINE() AS VARCHAR) AS ErrorLine      
  ,ERROR_MESSAGE() AS ProcessMessage      
 END CATCH      
 IF(OBJECT_ID('EnrollmentDB.#TEMP') Is Not Null)      
 BEGIN      
  DROP TABLE #TEMP      
 END       
END