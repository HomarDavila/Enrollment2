-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [ExportAses].[USP_UploadDataMembersRecords]   
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
 DECLARE @TransactionName VARCHAR(20) = 'PROCESS_TRANSACTION_MEMBERS_RECORD'  
  
 BEGIN TRY  
  SET @Procedure = OBJECT_NAME(@@PROCID)  
  SET @Start = GETDATE()  
  SET @SqlStatement = 'BULK INSERT #TEMP FROM ''' + @Exp_FilePath +   
  ''' WITH(ROWTERMINATOR = ''' + CHAR(10) + ''', CODEPAGE = ''' + 'ACP' + ''', DATAFILETYPE = ''' + 'WIDECHAR' + ''')'  
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
  INSERT INTO [ExportAses].[MembersRecords] (  
   TranId,  
   ProcessDate,  
   FamilySSN,  
   FamilySuffix,  
   MemberSSN,  
   MemberSuffix,  
   ContactMember,  
   LastName1,  
   LastName2,  
   FirstName,  
   MiddleName,  
   Relationship,  
   DateOfBirth,  
   PlaceOfBirth,  
   Sex,  
   Category,  
   Category2,  
   Condition,  
   SourceCode,  
   ReceiveSS,  
   MedInsCode,  
   Policy,  
   Class,  
   Class2,  
   DenialCat,  
   DenialCat2,  
   MaritalStatus,  
   SSN,  
   PregIND,  
   AbsentParent,  
   HICN,  
   PilotCat,  
   PilotClass,  
   PilotDenial,  
   HCREElegibilityIND,  
   HCREDenialCode,  
   OtherInsurer1,  
   OtherPolicy1,  
   OtherInsurer2,  
   OtherPolicy2,  
   OtherInsurer3,  
   OtherPolicy3,  
   GroupIdent,  
   MPI,  
   ELAErrors,  
   Agency,  
   MasterPatientIndex,  
   MemberCertificationDate,  
   ContractNumber,  
   MemberPrimaryCenter,  
   MPCEffectiveDate,  
   MemberNewPrimaryCenter,  
   MNPCEffectiveDate,  
   PCP1,  
   PCP1EffectiveDate,  
   PCP2,  
   PCP2EffectiveDate,  
   NewPCP1,  
   NewPCP1EffectiveDate,  
   NewPCP2,  
   NewPCP2EffectiveDate,  
   CardIdNumber,  
   CardIdDate,  
   ELAIndicator,  
   PrimaryCenterPCPChangeReason,  
   MedicaidIndicator,  
   MedicareIndicator,  
   Carrier,  
   CarrierEffectiveDate,  
   NewCarrier,  
   NewCarrierEffectiveDate,  
   PlanType,  
   PlanTypeEffectiveDate,  
   PlanVersion,  
   PlanVersionEffectiveDate,  
   NewPlanType,  
   NewPlanTypeEffectiveDate,  
   NewPlanVersion,  
   NewPlanVersionEffectiveDate,  
   InstitutionalStatus,  
   HICNumberMA,  
   AutoEnrollIndicator,  
   AutoEnrollDate,  
   IPAEspecial,  
   CMSCertStatus,  
   CoverageCode,  
   NewContractNumber,  
   SpecialEnroll,  
   CostSharingFlag,  
   MaxCoPay,  
   ExtensionFlag,  
   SpendDownFlag,  
   GroupCode,  
   BlockNumber,  
   ProcessHeaderId,  
   IsValidForImport,
   Deceased_Date
  )  
  SELECT   
   --SUBSTRING(Data,1,1) RECORD_TYPE,  
   SUBSTRING(Data,2,1) TRAN_ID,  
   SUBSTRING(Data,3,8) PROCESS_DATE,  
   SUBSTRING(Data,11,9) FAMILY_SSN,  
   SUBSTRING(Data,20,2) FAMILY_SUFFIX,  
   --SUBSTRING(Data,22,1) FILLER,  
   SUBSTRING(Data,23,9) MEMBER_SSN,  
   SUBSTRING(Data,32,2) MEMBER_SUFFIX,  
   SUBSTRING(Data,34,11) CONTACT_MEMBER,  
   --SUBSTRING(Data,45,3) FILLER,  
   SUBSTRING(Data,48,15) LAST_NAME_1,  
   SUBSTRING(Data,63,15) LAST_NAME_2,  
   SUBSTRING(Data,78,20) FIRST_NAME,  
   SUBSTRING(Data,98,1) MIDDLE_INITIAL,  
   SUBSTRING(Data,99,1) RELATIONSHIP,  
   SUBSTRING(Data,100,8) DATE_OF_BIRTH,  
   SUBSTRING(Data,108,1) PLACE_OF_BIRTH,  
   SUBSTRING(Data,109,1) SEX,  
   SUBSTRING(Data,110,1) CATEGORY,  
   SUBSTRING(Data,111,1) CATEGORY_2,  
   SUBSTRING(Data,112,1) CONDITION,  
   SUBSTRING(Data,113,1) SOURCE_CODE,  
   SUBSTRING(Data,114,1) RECEIVE_SS,  
   SUBSTRING(Data,115,1) MED_INS_CODE,  
   SUBSTRING(Data,116,2) POLICY,  
   SUBSTRING(Data,118,1) CLASS,  
   SUBSTRING(Data,119,1) CLASS_2,  
   SUBSTRING(Data,120,1) DENIAL_CAT,  
   SUBSTRING(Data,121,1) DENIAL_CAT_2,  
   SUBSTRING(Data,122,1) MARITAL_STATUS,  
   SUBSTRING(Data,123,9) SSN,  
   SUBSTRING(Data,132,1) PREG_IND,  
   SUBSTRING(Data,133,1) ABSENT_PARENT,  
   SUBSTRING(Data,134,11) HICN,  
   SUBSTRING(Data,145,1) PILOT_CAT,  
   SUBSTRING(Data,146,1) PILOT_CLASS,  
   SUBSTRING(Data,147,1) PILOT_DENIAL,  
   SUBSTRING(Data,148,1) HCRE_ELIGIBILITY_IND,  
   SUBSTRING(Data,149,2) HCRE_DENIAL_CODE,  
   SUBSTRING(Data,151,2) OTHER_INSURER1,  
   SUBSTRING(Data,153,20) OTH_POLICY1,  
   SUBSTRING(Data,173,2) OTHER_INSURER2,  
   SUBSTRING(Data,175,20) OTH_POLICY2,  
   SUBSTRING(Data,195,2) OTHER_INSURER3,  
   SUBSTRING(Data,197,20) OTH_POLICY3,  
   SUBSTRING(Data,217,2) GROUP_IDENT,  
   SUBSTRING(Data,219,11) MPI,  
   SUBSTRING(Data,230,10) ELA_ERRORS,  
   SUBSTRING(Data,240,5) AGENCY,  
   SUBSTRING(Data,245,13) MASTER_PATIENT_INDEX__MPI,  
   SUBSTRING(Data,258,8) MEMBER_CERTIFICATION_DATE,  
   SUBSTRING(Data,266,13) CONTRACT_NUMBER,  
   SUBSTRING(Data,279,4) MEMBER_PRIMARY_CENTER,  
   SUBSTRING(Data,283,8) MEMBER_PRIMARY_CENTER_EFFECTIVE_DATE,  
   SUBSTRING(Data,291,4) MEMBER_NEW_PRIMARY_CENTER,  
   SUBSTRING(Data,295,8) MEMBER_NEW_PRIMARY_CENTER_EFFECTIVE_DATE,  
   SUBSTRING(Data,303,15) PCP1,  
   SUBSTRING(Data,318,8) PCP1_EFFECTIVE_DATE,  
   SUBSTRING(Data,326,15) PCP2,  
   SUBSTRING(Data,341,8) PCP2_EFFECTIVE_DATE,  
   SUBSTRING(Data,349,15) NEW_PCP1,  
   SUBSTRING(Data,364,8) NEW_PCP1_EFFECTIVE_DATE,  
   SUBSTRING(Data,372,15) NEW_PCP2,  
   SUBSTRING(Data,387,8) NEW_PCP2_EFFECTIVE_DATE,  
   SUBSTRING(Data,395,15) CARD_ID_NUMBER,  
   SUBSTRING(Data,410,8) CARD_ID_DATE,  
   SUBSTRING(Data,418,1) ELA_INDICATOR,  
   SUBSTRING(Data,419,2) PRIMARY_CENTER_PCP_CHANGE_REASON,  
   SUBSTRING(Data,421,1) MEDICAID_INDICATOR,  
   SUBSTRING(Data,422,1) MEDICARE_INDICATOR,  
   SUBSTRING(Data,423,2) CARRIER,  
   SUBSTRING(Data,425,8) CARRIER_EFF_DATE,  
   SUBSTRING(Data,433,2) NEW_CARRIER,  
   SUBSTRING(Data,435,8) NEW_CARRIER_EFF_DATE,  
   SUBSTRING(Data,443,2) PLAN_TYPE,  
   SUBSTRING(Data,445,8) PLAN_TYPE_EFF_DATE,  
   SUBSTRING(Data,453,3) PLAN_VERSION,  
   SUBSTRING(Data,456,8) PLAN_VERSION_EFF_DATE,  
   SUBSTRING(Data,464,2) NEW_PLAN_TYPE,  
   SUBSTRING(Data,466,8) NEW_PLAN_TYPE_EFF_DATE,  
   SUBSTRING(Data,474,3) NEW_PLAN_VERSION,  
   SUBSTRING(Data,477,8) NEW_PLAN_VERSION_EFF_DATE,  
   SUBSTRING(Data,485,1) INSTITUTIONAL_STATUS,  
   SUBSTRING(Data,486,12) HIC_NUMBER_MA,  
   SUBSTRING(Data,498,1) AUTO_ENROLL_INDICATOR,  
   SUBSTRING(Data,499,8) AUTO_ENROLL_DATE,  
   SUBSTRING(Data,507,1) IPA_ESPECIAL,  
   SUBSTRING(Data,508,2) CMS_CERT_STATUS,  
   SUBSTRING(Data,510,3) COVERAGE_CODE,  
   SUBSTRING(Data,513,13) NEW_CONTRACT_NUMBER,  
   SUBSTRING(Data,526,1) SPECIAL_ENROLL,  
   SUBSTRING(Data,527,1) COST_SHARING_FLAG,  
   SUBSTRING(Data,528,5) MAX_COPAY,  
   SUBSTRING(Data,533,1) EXTENSION_FLAG,  
   SUBSTRING(Data,534,1) SPEND_DOWN_FLAG,  
   SUBSTRING(Data,535,3) GROUP_CODE,  
   
   --SUBSTRING(Data,546,194) FILLER,  
   SUBSTRING(Data,740,6) BLOCK_NUMBER,  
   @IdInserted,  
   1,
   SUBSTRING(Data,538,8) DECEASED_DATE
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