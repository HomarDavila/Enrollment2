-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [ExportAses].[USP_UploadDataInsuranceRecords]   
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
 DECLARE @TransactionName VARCHAR(20) = 'PROCESS_TRANSACTION_INSURANCE_RECORD'  
  
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
  INSERT INTO [ExportAses].[InsuranceRecords] (  
   TranId,  
   ProcessDate,  
   FamilyId,  
   MemberSuffix,  
   HealthInsurerCode,  
   PolicyNumber,  
   PolicyExpirationDate,  
   CoveredServices,  
   BlockNumber,  
   ProcessHeaderId,  
   IsValidForImport  
  )  
  SELECT   
   --SUBSTRING(Data,1,1) RECORD_TYPE,  
   SUBSTRING(Data,2,1) TRAN_ID,  
   SUBSTRING(Data,3,8) PROCESS_DATE,  
   SUBSTRING(Data,11,11) FAMILY_ID,  
   SUBSTRING(Data,22,2) MEMBER_SUFFIX,  
   SUBSTRING(Data,24,3) HEALTH_INSURER_CODE,  
   SUBSTRING(Data,27,20) POLICY_NUMBER,  
   SUBSTRING(Data,47,8) POLICY_EXPIRATION_DATE,  
   SUBSTRING(Data,55,40) COVERED_SERVICES,  
   --SUBSTRING(Data,95,445) FILLER,  
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