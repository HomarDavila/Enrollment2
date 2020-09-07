-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [ExportAses].[USP_UploadDataHouseHoldRecords]   
 @Exp_FilePath VARCHAR(500),  
 @Exp_FileName VARCHAR(100),  
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
 DECLARE @TransactionName VARCHAR(20) = 'PROCESS_TRANSACTION_HOUSEHOLD_RECORD'  
  
 BEGIN TRY  
  SET @Procedure = OBJECT_NAME(@@PROCID)  
  SET @Start = GETDATE()  
  SET @SqlStatement = 'BULK INSERT #TEMP FROM ''' + @Exp_FilePath +   
  ''' WITH(ROWTERMINATOR = ''' + CHAR(10) + ''', CODEPAGE = ''' + 'ACP' + ''')'  
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
   @Exp_FileName,  
   @ProcessType,  
   @Start,  
   NULL,  
   @UserName,  
   CONVERT(DATE, GETDATE()),  
   1,  
   NULL  
  )  
  SET @IdInserted = @@IDENTITY  
  INSERT INTO [ExportAses].[HouseHoldRecords] (  
   TranId,  
   ProcessDate,  
   MemberId,  
   MPI1,  
   MPI2,  
   MPI3,  
   MPI4,  
   MPI5,  
   MPI6,  
   MPI7,  
   MPI8,  
   MPI9,  
   MPI10,  
   MPI11,  
   MPI12,  
   MPI13,  
   MPI14,  
   MPI15,  
   MPI16,  
   MPI17,  
   MPI18,  
   BlockNumber,  
   ProcessHeaderId,  
   IsValidForImport  
  )  
  SELECT   
   --SUBSTRING(Data,1,1) RECORD_TYPE,  
   SUBSTRING(Data,2,1) TRAN_ID,  
   SUBSTRING(Data,3,8) PROCESS_DATE,  
   SUBSTRING(Data,11,11) MEMBER_ID,  
   SUBSTRING(Data,22,11) MPI_1,  
   SUBSTRING(Data,33,11) MPI_2,  
   SUBSTRING(Data,44,11) MPI_3,  
   SUBSTRING(Data,55,11) MPI_4,  
   SUBSTRING(Data,66,11) MPI_5,  
   SUBSTRING(Data,77,11) MPI_6,  
   SUBSTRING(Data,88,11) MPI_7,  
   SUBSTRING(Data,99,11) MPI_8,  
   SUBSTRING(Data,110,11) MPI_9,  
   SUBSTRING(Data,121,11) MPI_10,  
   SUBSTRING(Data,132,11) MPI_11,  
   SUBSTRING(Data,143,11) MPI_12,  
   SUBSTRING(Data,154,11) MPI_13,  
   SUBSTRING(Data,165,11) MPI_14,  
   SUBSTRING(Data,176,11) MPI_15,  
   SUBSTRING(Data,187,11) MPI_16,  
   SUBSTRING(Data,198,11) MPI_17,  
   SUBSTRING(Data,209,11) MPI_18,  
   --SUBSTRING(Data,220,320) FILLER,  
   SUBSTRING(Data,740,6) BLOCK_NUMBER,  
   @IdInserted,  
   1  
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
  ,ERROR_NUMBER() AS ErrorNumber  
  ,ERROR_PROCEDURE() AS ProcedureName  
  ,ERROR_LINE() AS ErrorLine  
  ,ERROR_MESSAGE() AS ProcessMessage  
 END CATCH  
 IF(OBJECT_ID('EnrollmentDB.#TEMP') Is Not Null)  
 BEGIN  
  DROP TABLE #TEMP  
 END   
END