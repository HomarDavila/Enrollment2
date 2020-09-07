--DECLARE     
-- @Sus_FileName VARCHAR(100) = '/FTP_ASES/FTP_Truenorth/Test/Submit to ASES/SUS/99200731.sus',      
-- @ProcessType VARCHAR(20) = 'GenerateFileForAses',      
-- @UserName VARCHAR(30) = 'SYSTEM'    
CREATE PROCEDURE [ExportAses].[USP_GenerateDataForAses]    
 @Sus_FileName VARCHAR(100),      
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
       
 DECLARE @TransactionName VARCHAR(50) = 'PROCESS_TRANSACTION_EXPORT_FILE_FOR_ASES'      
      
 BEGIN TRY      
  SET @Procedure = OBJECT_NAME(@@PROCID)      
  SET @Start = GETDATE()    
    
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
   @Sus_FileName,      
   @ProcessType,      
   @Start,      
   NULL,      
   @UserName,      
   CONVERT(DATE, GETDATE()),      
   1,      
   NULL      
  )     
    
  SET @IdInserted = @@IDENTITY      
  INSERT INTO [ExportAses].[ExportFileForAses] (      
  [RECORD_TYPE]    
        ,[TRAN_ID]    
        ,[PROCESS_DATE]    
        ,[REGION]    
        ,[CARRIER]    
        ,[MEMBER_PRIMARY_CENTER]    
        ,[ODSI_FAMILY_ID]    
        ,[MEMBER_SSN]    
        ,[MEMBER_SUFFIX]    
        ,[EFFECTIVE_DATE]    
        ,[PLAN_TYPE]    
        ,[PLAN_VERSION]    
        ,[MPI]    
        ,[PCP1]    
        ,[PCP1_EFFECTIVE_DATE]    
        ,[PCP2]    
        ,[PCP2_EFFECTIVE_DATE]    
        ,[FAMILY_PRIMARY_CENTER]    
        ,[PMG_TAX_ID_EFF_DT]    
        ,[IPA_PCP_CHANGE_REASON]    
        ,[MEDICARE_INDICATOR]    
        ,[HIC_NUMBER]    
        ,[REJECT_IDENTIFIER]    
        ,[RECORD_KEY]    
        ,[ERROR_CODE_1]    
        ,[ERROR_CODE_2]    
        ,[ERROR_CODE_3]    
        ,[ERROR_CODE_4]    
        ,[ERROR_CODE_5]    
        ,[ERROR_CODE_6]    
        ,[ERROR_CODE_7]    
        ,[ERROR_CODE_8]    
        ,[ERROR_CODE_9]    
        ,[ERROR_CODE_10]    
        ,[UPDATE_DATE]    
        ,[UPDATE_USER]    
        ,[IPA_ESPECIAL]    
        ,[CONTRACT_NUMBER]    
        ,[SPECIAL_ENROLL]    
        ,[PMG_TAX_ID]    
        ,[DATA_SOURCE]    
        ,[FILLER]    
        ,[BLOCK_NUMBER]    
        ,[PROCESS_HEADER_ID]    
        ,[GENERATED_FOR_ASES]    
        ,[GENERATED_FOR_ASES_DATE]    
        ,[REJECTED_FROM_ASES]    
        ,[REJECTED_FROM_ASES_DATE]    
        ,[MEMBER_ID_ENROLLMENTHISTORY]  
  ,[STATUSID]    
  )      
  SELECT       
   'E',      
   'C',      
   FORMAT(EH.CreatedOn, 'MMddyyyy'),    
   RIGHT(CONCAT(replicate(' ', 1), F.[Region]), 1),    
   MCO.[CarrierCode],    
   CASE WHEN LEN(LTRIM(RTRIM(EH.[MemberPrimaryCenter]))) > 4 THEN replicate(' ', 4) ELSE RIGHT(CONCAT(replicate(' ', 4), LTRIM(RTRIM(EH.[MemberPrimaryCenter]))), 4) END,  
   RIGHT(CONCAT(replicate(' ', 11), EH.MPIshort), 11),    
   RIGHT(CONCAT(replicate(' ', 9), EH.[SSN]), 9),    
   '01',    
   FORMAT(EH.[MCOEffectiveDate], 'MMddyyyy'),    
   '01',    
   RIGHT(CONCAT(replicate(' ', 3), M.[CoverageCode]), 3),    
   RIGHT(CONCAT(replicate(' ', 13), EH.[MPI]), 13),    
   LEFT(CONCAT(PCP.NPI, replicate(' ', 15)), 15),    
   FORMAT(EH.[PCPEffectiveDate], 'MMddyyyy'),    
   replicate(' ', 15) ,    
   replicate(' ', 8),    
   CASE WHEN LEN(LTRIM(RTRIM(EH.[MemberPrimaryCenter]))) > 4 THEN replicate(' ', 4) ELSE RIGHT(CONCAT(replicate(' ', 4), LTRIM(RTRIM(EH.[MemberPrimaryCenter]))), 4) END,   
   FORMAT(EH.[PMGEffectiveDate], 'MMddyyyy'),    
   replicate(' ', 2),    
   replicate(' ', 1),    
   replicate(' ', 12),    
   replicate(' ', 1),    
   replicate(' ', 14),    
   replicate(' ', 3),    
   replicate(' ', 3),    
   replicate(' ', 3),    
   replicate(' ', 3),    
   replicate(' ', 3),    
   replicate(' ', 3),    
   replicate(' ', 3),    
   replicate(' ', 3),    
   replicate(' ', 3),    
   replicate(' ', 3),    
   FORMAT(GETDATE(), 'MMddyyyy'),    
   'SYSTUPD ',    
   replicate(' ', 1),    
   RIGHT(CONCAT(replicate(' ', 13), EH.[MPI]), 13),    
   replicate(' ', 1),    
   RIGHT(CONCAT(replicate(' ', 9), PMG.[PmgFederalTaxId]), 9),    
   CASE WHEN EH.JustCauseReasoNId = 0 THEN 'CO' ELSE 'JC' END,   
   replicate(' ', 4),    
   NULL,    
   @IdInserted,    
   NULL,    
   NULL,    
   NULL,    
   NULL,    
   EH.Id,        
   EH.StatusId  
  FROM [Enrollment].[EnrollmentHistories] EH (NOLOCK)    
  INNER JOIN [Enrollment].[Members] M (NOLOCK) ON M.ID= EH.MemberId     
  INNER JOIN [Enrollment].[Families] F (NOLOCK) ON EH.FamilyId = F.Id     
  INNER JOIN [Enrollment].[ManagedCareOrganizations] MCO (NOLOCK) ON EH.MCOId = MCO.Id    
  --INNER JOIN [Enrollment].PrimaryCarePhysician PCP (NOLOCK) ON EH.PCPId = PCP.Id    
  INNER JOIN [Enrollment].[PersonPrimaryCarePhysician] PCP (NOLOCK) ON EH.PCPId = PCP.Id    
  INNER JOIN [Enrollment].[PrimaryMedicalGroup] PMG (NOLOCK) ON EH.PMGId = PMG.Id    
  WHERE EH.StatusId=1  

  SET @Rows = @@ROWCOUNT      
  SET @Error = @@ERROR      
      
  UPDATE [ExportAses].[ProcessHeader]      
  SET EndTime = GETDATE()      
  WHERE ID = @IdInserted      
      
  COMMIT TRAN @TransactionName      
      
  SELECT      
  CAST(@IdInserted as varchar(10)) As ProcessHeaderId      
  ,@ProcessType AS ProcessType      
  ,CAST(@Error as varchar(10)) AS ErrorNumber      
  ,@Procedure AS ProcedureName      
  ,NULL AS ErrorLine      
  ,CAST(@Rows as varchar(10)) + ' Affected Rows' AS ProcessMessage      
 END TRY      
 BEGIN CATCH      
  ROLLBACK TRAN @TransactionName      
  SELECT      
   NULL As ProcessHeaderId      
  ,@ProcessType AS ProcessType      
  ,CAST(ERROR_NUMBER() as varchar(10)) AS ErrorNumber      
  ,ERROR_PROCEDURE() AS ProcedureName      
  ,CAST(ERROR_LINE() as varchar(10)) AS ErrorLine      
  ,ERROR_MESSAGE() AS ProcessMessage      
 END CATCH     
END