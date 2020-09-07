-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [ExportAses].[USP_UploadDataFamilyRecords]     
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
 DECLARE @TransactionName VARCHAR(20) = 'PROCESS_TRANSACTION_FAMILY_RECORD'    
    
 BEGIN TRY    
  SET @Procedure = OBJECT_NAME(@@PROCID)    
  SET @Start = GETDATE()    
  SET @SqlStatement = 'BULK INSERT #TEMP FROM ''' + @Exp_FilePath +     
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
  INSERT INTO [ExportAses].[FamilyRecords] (    
   TranId,    
   ProcessDate,    
   FamilySSN,    
   FamilySuffix,    
   FamilyId,    
   ContactLastName1,    
   ContactLastName2,    
   ContactFirstName,    
   Region,    
   Municipality,    
   Facility,    
   InvestigationInd,    
   TransactionType,    
   EffectiveDate,    
   FinancialRespPct,    
   CertifierNumber,    
   ExpirationDate,    
   CondEligInd,    
   MailingAddress1,    
   MailingAddress2,    
   MailingCity,    
   MailingZip,    
   MalingZip4,    
   ResidenceAddress1,    
   ResidenceAddress2,    
   ResidenceCity,    
   ResidenceZip,    
   ResidenceZip4,    
   Phone,    
   OtherInsurer1,    
   OtherPolicy1,    
   OtherInsurer2,    
   OtherPolicy2,    
   OtherInsurer3,    
   OtherPolicy3,    
   Members,    
   OdsiMembersElegible,    
   UserCode,    
   EntryDate,    
   PctofPovertylevel,    
   DeductibleLevelCode,    
   HCREMembersElegible,    
   HCREDenialCode,    
   CarrierCode,    
   EffectiveCarrierDate,    
   ElaErrors,    
   Mancomunado,    
   PmgTaxId,    
   NewCarrier,    
   NewPMGTaxId,    
   NewPMGEffectiveDate,    
   ContractNumber,    
   RegionASES,    
   NewCarrierEffectiveDate,    
   PMGEffectiveDate,    
   CertificationDate,    
   PrimaryCenterPCPChangeReason,    
   AutoEnrollIndicator,    
   AutoEnrollDate,    
   PamNewFamilyId,    
   ApplicationNumber,    
   MedicaidCancelationDt,    
   RegionMoveEffDt,    
   RateCell,    
   Gender,    
   NewCardIdDate,    
   BlockNumber,    
   ProcessHeaderId,    
   IsValidForImport    
  )    
  SELECT     
   --SUBSTRING(Data,1,1) RECORD_TYPE,    
   SUBSTRING(Data,2,1) TRAN_ID,    
   SUBSTRING(Data,3,8) PROCESS_DATE,    
   SUBSTRING(Data,11,9) FAMILY_SSN,    
   SUBSTRING(Data,20,2) FAMILY_SUFFIX,    
   --SUBSTRING(Data,22,14) FILLER_,    
   SUBSTRING(Data,36,11) FAMILY_ID,    
   SUBSTRING(Data,47,15) CONTACT_LAST_NAME_1,    
   SUBSTRING(Data,62,15) CONTACT_LAST_NAME_2,    
   SUBSTRING(Data,77,20) CONTACT_FIRST_NAME,    
   SUBSTRING(Data,97,1) REGION,    
   SUBSTRING(Data,98,4) MUNICIPALITY,    
   SUBSTRING(Data,102,4) FACILITY,    
   SUBSTRING(Data,106,1) INVESTIGATION_IND,    
   SUBSTRING(Data,107,1) TRANSACTION_TYPE,    
   SUBSTRING(Data,108,8) EFFECTIVE_DATE,    
   SUBSTRING(Data,116,1) FINANCIAL_RESP_PCT,    
   SUBSTRING(Data,117,2) CERTIFIER_NUMBER,    
   SUBSTRING(Data,119,8) EXPIRATION_DATE,    
   SUBSTRING(Data,127,1) COND_ELIG_IND,    
   --SUBSTRING(Data,128,25) MAILING_ADDRESS1,    
   SUBSTRING(Data,128,75) MAILING_ADDRESS1,   
   --SUBSTRING(Data,153,25) MAILING_ADDRESS2,   
   SUBSTRING(Data,203,75) MAILING_ADDRESS2,   
   SUBSTRING(Data,278,16) MAILING_CITY,    
   SUBSTRING(Data,294,5) MAILING_ZIP,    
   SUBSTRING(Data,299,4) MAILING_ZIP4,    
   --SUBSTRING(Data,203,25) RESIDENCE_ADDRESS1,    
   SUBSTRING(Data,303,75) RESIDENCE_ADDRESS1,    
   --SUBSTRING(Data,228,25) RESIDENCE_ADDRESS2,    
   SUBSTRING(Data,378,75) RESIDENCE_ADDRESS2,    
   SUBSTRING(Data,453,16) RESIDENCE_CITY,    
   SUBSTRING(Data,469,5) RESIDENCE_ZIP,    
   SUBSTRING(Data,474,4) RESIDENCE_ZIP4,    
   SUBSTRING(Data,478,10) PHONE,    
   SUBSTRING(Data,488,2) OTHER_INSURER1,    
   SUBSTRING(Data,490,20) OTH_POLICY1,    
   SUBSTRING(Data,510,2) OTHER_INSURER2,    
   SUBSTRING(Data,512,20) OTH_POLICY2,    
   SUBSTRING(Data,532,2) OTHER_INSURER3,    
   SUBSTRING(Data,534,20) OTH_POLICY3,    
   SUBSTRING(Data,554,2) MEMBERS,    
   SUBSTRING(Data,556,2) ODSI_MEMBERS_ELIGIBLE,    
   SUBSTRING(Data,558,6) USER_CODE,    
   SUBSTRING(Data,564,8) ENTRY_DATE,    
   SUBSTRING(Data,572,3) PCT_OF_POVERTY_LEVEL,    
   SUBSTRING(Data,575,1) DEDUCTIBLE_LEVEL_CODE,    
   SUBSTRING(Data,576,2) HCRE_MEMBERS_ELIGIBLE,    
   SUBSTRING(Data,578,2) HCRE_DENIAL_CODE,    
   SUBSTRING(Data,580,2) CARRIER_CODE,    
   SUBSTRING(Data,582,8) EFFECTIVE_CARRIER_DATE,    
   SUBSTRING(Data,590,10) ELA_ERRORS,    
   SUBSTRING(Data,600,1) MANCOMUNADO,    
   --SUBSTRING(Data,401,3) FILLER,    
   SUBSTRING(Data,604,9) PMG_TAX_ID,    
   SUBSTRING(Data,613,2) NEW_CARRIER,    
   SUBSTRING(Data,615,9) NEW_PMG_TAX_ID,    
   SUBSTRING(Data,624,8) NEW_PMG_EFF_DATE,    
   SUBSTRING(Data,632,13) CONTRACT_NUMBER,    
   SUBSTRING(Data,645,1) REGION_ASES,    
   SUBSTRING(Data,646,8) NEW_CARRIER_EFFECTIVE_DATE,    
   SUBSTRING(Data,654,8) PMG_EFF_DATE,    
   SUBSTRING(Data,662,8) CERTIFICATION_DATE,    
   SUBSTRING(Data,670,2) PRIMARY_CENTER_PCP_CHANGE_REASON,    
   SUBSTRING(Data,672,1) AUTO_ENROLL_INDICATOR,    
   SUBSTRING(Data,673,8) AUTO_ENROLL_DATE,    
   SUBSTRING(Data,681,11) PAM_NEW_FAMILY_ID,    
   SUBSTRING(Data,692,10) APPLICATION_NUMBER,    
   SUBSTRING(Data,702,8) MEDICAID_CANCELLATION_DT,    
   SUBSTRING(Data,510,8) REGION_MOVE_EFF_DT,    
   SUBSTRING(Data,718,2) RATE_CELL,    
   SUBSTRING(Data,720,1) GENDER,    
   SUBSTRING(Data,721,8) NEW_CARD_ID_DATE,    
   --SUBSTRING(Data,529,11) FILLER,    
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