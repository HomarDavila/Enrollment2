--declare
-- @FamilyProcessId int=38,     
-- @MemberProcessId int=39,     
-- @ProcessType VARCHAR(20)='fhgf',    
-- @UserName VARCHAR(30)='system' 
CREATE PROCEDURE [ExportAses].[ExecuteNormalizationBeneficiaries]       
 @FamilyProcessId int,     
 @MemberProcessId int,     
 @ProcessType VARCHAR(20),    
 @UserName VARCHAR(30)    
AS       
BEGIN          
 SET NOCOUNT ON;    
 DECLARE @McoId INT    
 DECLARE @McoEffectiveDate DATE    
 DECLARE @PmgId INT    
 DECLARE @PmgEffectiveDate DATE    
 DECLARE @PcpId INT    
 DECLARE @PcpEffectiveDate DATE    
 DECLARE @MPI VARCHAR(100)    
      
 DECLARE @SqlStatement VARCHAR(500)    
 DECLARE @IdInserted INT    
 DECLARE @Procedure VARCHAR(100)    
 DECLARE @Rows int    
 DECLARE @Error VARCHAR(300)    
 DECLARE @Start DATETIME    
 DECLARE @End DATETIME    
 DECLARE @TransactionName VARCHAR(20) = 'PROCESS_TRANSACTION'    
 DECLARE @trancount int    
 BEGIN TRY    
  SET @Procedure = OBJECT_NAME(@@PROCID)    
  SET @Start = GETDATE()      
  SET @trancount = @@trancount    
      
  IF @trancount = 0    
            BEGIN TRAN    
        else    
            SAVE TRAN @TransactionName    
      
  --Update tablas    
  UPDATE [ExportAses].[FamilyRecords]    
     SET [TranId] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF(TranId,''), NULL))),2)    
     ,[FamilySSN] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([FamilySSN],''), NULL))),9)    
     ,[FamilySuffix] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([FamilySuffix],''), NULL))), 2)    
     ,[FamilyId] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([FamilyId],''), NULL))), 11)    
     ,[ContactLastName1] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([ContactLastName1],''), NULL))), 50)    
     ,[ContactLastName2] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([ContactLastName2],''), NULL))), 50)    
     ,[ContactFirstName] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([ContactFirstName],''), NULL))), 100)    
     ,[Region] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([Region],''), NULL))), 2)    
     ,[Municipality] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([Municipality],''), NULL))), 4)    
     ,[Facility] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([Facility],''), NULL))), 4)       
     ,[EffectiveDate] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([EffectiveDate],''), NULL))), 8)         
     ,[ExpirationDate] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([ExpirationDate],''), NULL))), 8)         
     ,[CertificationDate] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([CertificationDate],''), NULL))), 8)         
     ,[MailingAddress1] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MailingAddress1],''), NULL))), 50)         
     ,[MailingAddress2] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MailingAddress2],''), NULL))), 50)         
     ,[MailingCity] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MailingCity],''), NULL))), 50)         
     ,[MailingZip] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MailingZip],''), NULL))), 50)         
     ,[MalingZip4] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MalingZip4],''), NULL))), 4)         
     ,[ResidenceAddress1] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([ResidenceAddress1],''), NULL))), 50)         
     ,[ResidenceAddress2] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([ResidenceAddress2],''), NULL))), 50)         
     ,[ResidenceCity] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([ResidenceCity],''), NULL))), 50)         
     ,[ResidenceZip] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([ResidenceZip],''), NULL))), 50)         
     ,[ResidenceZip4] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([ResidenceZip4],''), NULL))), 10)         
     ,[Phone] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([Phone],''), NULL))), 20)              
  WHERE ProcessHeaderId = @FamilyProcessId    
  AND [IsValidForImport] = 1    
    
  UPDATE [ExportAses].[MembersRecords]    
     SET [TranId] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF(TranId,''), NULL))),2)    
     ,[MemberSSN] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MemberSSN],''), NULL))),9)    
     ,[MemberSuffix] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MemberSuffix],''), NULL))),2)    
     ,[ContactMember] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([ContactMember],''), NULL))),11)    
     ,[LastName1] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([LastName1],''), NULL))),50)    
     ,[LastName2] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([LastName2],''), NULL))),50)    
     ,[FirstName] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([FirstName],''), NULL))),100)    
     ,[MiddleName] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MiddleName],''), NULL))),50)    
     ,[DateOfBirth] =  LEFT(RTRIM(LTRIM(ISNULL(NULLIF([DateOfBirth],''), NULL))), 8)         
     ,[SSN] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([SSN],''), NULL))),9)         
     ,[MPI] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MPI],''), NULL))),11)         
     ,[MasterPatientIndex] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MasterPatientIndex],''), NULL))),13)         
     ,[MemberCertificationDate] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MemberCertificationDate],''), NULL))),8)         
     ,[PlanType] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([PlanType],''), NULL))),2)         
     ,[PlanVersion] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([PlanVersion],''), NULL))),2)     
     ,[MemberPrimaryCenter] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MemberPrimaryCenter],''), NULL))),4)     
     ,[MedicaidIndicator] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MedicaidIndicator],''), NULL))),2)     
     ,[MedicareIndicator] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([MedicareIndicator],''), NULL))),2)     
     ,[HICNumberMA] = LEFT(RTRIM(LTRIM(ISNULL(NULLIF([HICNumberMA],''), NULL))),12)     
   WHERE ProcessHeaderId = @MemberProcessId    
   AND [IsValidForImport] = 1    
             
  --Borrando tablas temporales    
  IF OBJECT_ID('tempdb..#ParsedFamilyData') IS NOT NULL    
            BEGIN    
                DROP TABLE #ParsedFamilyData;    
            END;    
  IF OBJECT_ID('tempdb..#ParsedMemberData') IS NOT NULL    
            BEGIN    
                DROP TABLE #ParsedMemberData;    
            END;    
  IF OBJECT_ID('tempdb..#ASESUpdateData') IS NOT NULL    
            BEGIN    
                DROP TABLE #ASESUpdateData;    
            END;    
        IF OBJECT_ID('tempdb..#UpdateCandidates') IS NOT NULL    
            BEGIN    
                DROP TABLE #UpdateCandidates;    
            END;    
        
  --Creando tablas temporales    
  CREATE TABLE #ParsedFamilyData(    
   [TranId] [nchar](10) NULL,    
   [ProcessDate] [nchar](8) NULL,    
   [FamilySSN] [nchar](10) NULL,    
   [FamilySuffix] [nchar](10) NULL,    
   [FamilyId] [nvarchar](11) NULL,    
   [ContactLastName1] [nvarchar](15) NULL,    
   [ContactLastName2] [nvarchar](15) NULL,    
   [ContactFirstName] [nvarchar](20) NULL,    
   [Region] [nvarchar](2) NULL,    
   [Municipality] [nvarchar](4) NULL,    
   [Facility] [nvarchar](4) NULL,    
   [InvestigationInd] [nvarchar](2) NULL,    
   [TransactionType] [nvarchar](2) NULL,    
   [EffectiveDate] [nvarchar](10) NULL,    
   [FinancialRespPct] [nvarchar](2) NULL,    
   [CertifierNumber] [nvarchar](2) NULL,    
   [ExpirationDate] [nvarchar](8) NULL,    
   [CondEligInd] [nvarchar](2) NULL,    
   [MailingAddress1] [nvarchar](75) NULL,    
   [MailingAddress2] [nvarchar](75) NULL,    
   [MailingCity] [nvarchar](16) NULL,    
   [MailingZip] [nvarchar](5) NULL,    
   [MalingZip4] [nchar](4) NULL,    
   [ResidenceAddress1] [nvarchar](75) NULL,    
   [ResidenceAddress2] [nvarchar](75) NULL,    
   [ResidenceCity] [nvarchar](16) NULL,    
   [ResidenceZip] [nvarchar](5) NULL,    
   [ResidenceZip4] [nvarchar](4) NULL,    
   [Phone] [nvarchar](10) NULL,    
   [OtherInsurer1] [nvarchar](2) NULL,    
   [OtherPolicy1] [nvarchar](20) NULL,    
   [OtherInsurer2] [nvarchar](2) NULL,    
   [OtherPolicy2] [nvarchar](20) NULL,    
   [OtherInsurer3] [nvarchar](2) NULL,    
   [OtherPolicy3] [nvarchar](20) NULL,    
   [Members] [nvarchar](2) NULL,    
   [OdsiMembersElegible] [nvarchar](2) NULL,    
   [UserCode] [nvarchar](6) NULL,    
   [EntryDate] [nvarchar](10) NULL,    
   [PctofPovertylevel] [nvarchar](3) NULL,    
   [DeductibleLevelCode] [nvarchar](2) NULL,    
   [HCREMembersElegible] [nvarchar](2) NULL,    
   [HCREDenialCode] [nvarchar](2) NULL,    
   [CarrierCode] [nvarchar](2) NULL,    
   [EffectiveCarrierDate] [nvarchar](10) NULL,    
   [ElaErrors] [nvarchar](10) NULL,    
   [Mancomunado] [nvarchar](2) NULL,    
   [PmgTaxId] [nvarchar](9) NULL,    
   [NewCarrier] [nvarchar](2) NULL,    
   [NewPMGTaxId] [nvarchar](9) NULL,    
   [NewPMGEffectiveDate] [nvarchar](10) NULL,    
   [ContractNumber] [nvarchar](13) NULL,    
   [RegionASES] [nvarchar](2) NULL,    
   [NewCarrierEffectiveDate] [nvarchar](10) NULL,    
   [PMGEffectiveDate] [nvarchar](10) NULL,    
   [CertificationDate] [nvarchar](10) NULL,    
   [PrimaryCenterPCPChangeReason] [nvarchar](2) NULL,    
   [AutoEnrollIndicator] [nvarchar](2) NULL,    
   [AutoEnrollDate] [nvarchar](8) NULL,    
   [PamNewFamilyId] [nvarchar](11) NULL,    
   [ApplicationNumber] [nvarchar](10) NULL,    
   [MedicaidCancelationDt] [nvarchar](10) NULL,    
   [RegionMoveEffDt] [nvarchar](10) NULL,    
   [RateCell] [nvarchar](2) NULL,    
   [Gender] [nvarchar](2) NULL,    
   [NewCardIdDate] [nvarchar](10) NULL,    
   [BlockNumber] [nchar](6) NULL,    
   [ProcessHeaderId] [int] NULL,    
   [IsValidForImport] [bit] NULL)    
      
  CREATE TABLE #ParsedMemberData(    
   [TranId] [nchar](2) NULL,    
   [ProcessDate] [nvarchar](10) NULL,    
   [FamilySSN] [nvarchar](10) NULL,    
   [FamilySuffix] [nvarchar](2) NULL,    
   [MemberSSN] [nvarchar](10) NULL,    
   [MemberSuffix] [nvarchar](2) NULL,    
   [ContactMember] [nvarchar](11) NULL,    
   [LastName1] [nvarchar](15) NULL,    
   [LastName2] [nvarchar](15) NULL,    
   [FirstName] [nvarchar](20) NULL,    
   [MiddleName] [nvarchar](2) NULL,    
   [Relationship] [nvarchar](2) NULL,    
   [DateOfBirth] [nvarchar](10) NULL,    
   [PlaceOfBirth] [nvarchar](2) NULL,    
   [Sex] [nvarchar](2) NULL,    
   [Category] [nvarchar](2) NULL,    
   [Category2] [nvarchar](2) NULL,    
   [Condition] [nvarchar](2) NULL,    
   [SourceCode] [nvarchar](2) NULL,    
   [ReceiveSS] [nvarchar](2) NULL,    
   [MedInsCode] [nvarchar](2) NULL,    
   [Policy] [nvarchar](2) NULL,    
   [Class] [nvarchar](2) NULL,    
   [Class2] [nvarchar](2) NULL,    
   [DenialCat] [nvarchar](2) NULL,    
   [DenialCat2] [nvarchar](2) NULL,    
   [MaritalStatus] [nvarchar](2) NULL,    
   [SSN] [nvarchar](9) NULL,    
   [PregIND] [nvarchar](2) NULL,    
   [AbsentParent] [nvarchar](2) NULL,    
   [HICN] [nvarchar](11) NULL,    
   [PilotCat] [nvarchar](2) NULL,    
   [PilotClass] [nvarchar](2) NULL,    
   [PilotDenial] [nvarchar](2) NULL,    
   [HCREElegibilityIND] [nvarchar](2) NULL,    
   [HCREDenialCode] [nvarchar](2) NULL,    
   [OtherInsurer1] [nvarchar](2) NULL,    
   [OtherPolicy1] [nvarchar](20) NULL,    
   [OtherInsurer2] [nchar](10) NULL,    
   [OtherPolicy2] [nvarchar](20) NULL,    
   [OtherInsurer3] [nchar](10) NULL,    
   [OtherPolicy3] [nvarchar](20) NULL,    
   [GroupIdent] [nvarchar](2) NULL,    
   [MPI] [nvarchar](11) NULL,    
   [ELAErrors] [nvarchar](20) NULL,    
   [Agency] [nvarchar](5) NULL,    
   [MasterPatientIndex] [nvarchar](13) NULL,    
   [MemberCertificationDate] [nvarchar](10) NULL,    
   [ContractNumber] [nvarchar](13) NULL,    
   [MemberPrimaryCenter] [nvarchar](4) NULL,    
   [MPCEffectiveDate] [nvarchar](10) NULL,    
   [MemberNewPrimaryCenter] [nvarchar](4) NULL,    
   [MNPCEffectiveDate] [nvarchar](10) NULL,    
   [PCP1] [nvarchar](15) NULL,    
   [PCP1EffectiveDate] [nvarchar](10) NULL,    
   [PCP2] [nvarchar](15) NULL,    
   [PCP2EffectiveDate] [nvarchar](10) NULL,    
   [NewPCP1] [nvarchar](15) NULL,    
   [NewPCP1EffectiveDate] [nvarchar](10) NULL,    
   [NewPCP2] [nvarchar](15) NULL,    
   [NewPCP2EffectiveDate] [nvarchar](10) NULL,    
   [CardIdNumber] [nvarchar](15) NULL,    
   [CardIdDate] [nvarchar](10) NULL,    
   [ELAIndicator] [nvarchar](2) NULL,    
   [PrimaryCenterPCPChangeReason] [nvarchar](2) NULL,    
   [MedicaidIndicator] [nvarchar](2) NULL,    
   [MedicareIndicator] [nvarchar](2) NULL,    
   [Carrier] [nvarchar](2) NULL,    
   [CarrierEffectiveDate] [nvarchar](10) NULL,    
   [NewCarrier] [nvarchar](2) NULL,    
   [NewCarrierEffectiveDate] [nvarchar](10) NULL,    
   [PlanType] [nvarchar](2) NULL,    
   [PlanTypeEffectiveDate] [nvarchar](10) NULL,    
   [PlanVersion] [nvarchar](3) NULL,    
   [PlanVersionEffectiveDate] [nvarchar](10) NULL,    
   [NewPlanType] [nvarchar](2) NULL,    
   [NewPlanTypeEffectiveDate] [nvarchar](10) NULL,    
   [NewPlanVersion] [nvarchar](3) NULL,    
   [NewPlanVersionEffectiveDate] [nvarchar](10) NULL,    
   [InstitutionalStatus] [nvarchar](2) NULL,    
   [HICNumberMA] [nvarchar](12) NULL,    
   [AutoEnrollIndicator] [nvarchar](2) NULL,    
   [AutoEnrollDate] [nvarchar](10) NULL,    
   [IPAEspecial] [nvarchar](2) NULL,    
   [CMSCertStatus] [nvarchar](2) NULL,    
   [CoverageCode] [nvarchar](3) NULL,    
   [NewContractNumber] [nvarchar](13) NULL,    
   [SpecialEnroll] [nvarchar](2) NULL,    
   [CostSharingFlag] [nvarchar](2) NULL,    
   [MaxCoPay] [nvarchar](5) NULL,    
   [ExtensionFlag] [nvarchar](2) NULL,    
   [SpendDownFlag] [nvarchar](2) NULL,    
   [GroupCode] [nvarchar](3) NULL,    
   [BlockNumber] [nchar](6) NULL,    
   [ProcessHeaderId] [int] NULL,    
   [IsValidForImport] [bit] NULL)    
      
  CREATE TABLE #ASESUpdateData    
            (Id  INT PRIMARY KEY  IDENTITY(1000000, 1)    
            ,[Index] [NCHAR](6) NULL    
            ,ProcessHeaderId [INT] NOT NULL    
            ,[MPI] [VARCHAR](11) NULL    
            ,[MCOId] INT NULL                
            ,[MCOEffectiveDate] [DATE] NULL    
            ,[PMGId] INT NULL                
            ,[PMGEffectiveDate] [DATE] NULL    
   ,[PCPId] INT NULL                
            ,[PCPEffectiveDate] [DATE] NULL                            
            ,INDEX IDXpfd1 NONCLUSTERED ( MPI )    
            );     
      
  INSERT INTO #ParsedFamilyData    
  SELECT [TranId]    
     ,[ProcessDate]    
     ,[FamilySSN]    
     ,[FamilySuffix]    
     ,[FamilyId]    
     ,[ContactLastName1]    
     ,[ContactLastName2]    
     ,[ContactFirstName]    
     ,[Region]    
     ,[Municipality]    
     ,[Facility]    
     ,[InvestigationInd]    
     ,[TransactionType]    
     ,[EffectiveDate]    
     ,[FinancialRespPct]    
     ,[CertifierNumber]    
     ,[ExpirationDate]    
     ,[CondEligInd]    
     ,[MailingAddress1]    
     ,[MailingAddress2]    
     ,[MailingCity]    
     ,[MailingZip]    
     ,[MalingZip4]    
     ,[ResidenceAddress1]    
     ,[ResidenceAddress2]    
     ,[ResidenceCity]    
     ,[ResidenceZip]    
     ,[ResidenceZip4]    
     ,[Phone]    
     ,[OtherInsurer1]    
     ,[OtherPolicy1]    
     ,[OtherInsurer2]    
     ,[OtherPolicy2]    
     ,[OtherInsurer3]    
     ,[OtherPolicy3]    
     ,[Members]    
     ,[OdsiMembersElegible]    
     ,[UserCode]    
     ,[EntryDate]    
     ,[PctofPovertylevel]    
     ,[DeductibleLevelCode]    
     ,[HCREMembersElegible]    
     ,[HCREDenialCode]    
     ,[CarrierCode]    
     ,[EffectiveCarrierDate]    
     ,[ElaErrors]    
     ,[Mancomunado]    
     ,[PmgTaxId]    
     ,[NewCarrier]    
     ,[NewPMGTaxId]    
     ,[NewPMGEffectiveDate]    
     ,[ContractNumber]    
     ,[RegionASES]    
     ,[NewCarrierEffectiveDate]    
     ,[PMGEffectiveDate]    
     ,[CertificationDate]    
     ,[PrimaryCenterPCPChangeReason]    
     ,[AutoEnrollIndicator]    
     ,[AutoEnrollDate]    
     ,[PamNewFamilyId]    
     ,[ApplicationNumber]    
     ,[MedicaidCancelationDt]    
     ,[RegionMoveEffDt]    
     ,[RateCell]    
     ,[Gender]    
     ,[NewCardIdDate]    
     ,[BlockNumber]    
     ,[ProcessHeaderId]    
     ,[IsValidForImport]    
  FROM [ExportAses].[FamilyRecords]    
  WHERE ProcessHeaderId = @FamilyProcessId    
  AND [IsValidForImport] = 1    
      
  INSERT INTO #ParsedMemberData    
  SELECT[TranId]    
     ,[ProcessDate]    
     ,[FamilySSN]    
     ,[FamilySuffix]    
     ,[MemberSSN]    
     ,[MemberSuffix]    
     ,[ContactMember]    
     ,[LastName1]    
     ,[LastName2]    
     ,[FirstName]    
     ,[MiddleName]    
     ,[Relationship]    
     ,[DateOfBirth]    
     ,[PlaceOfBirth]    
     ,[Sex]    
     ,[Category]    
     ,[Category2]    
     ,[Condition]    
     ,[SourceCode]    
     ,[ReceiveSS]    
     ,[MedInsCode]    
     ,[Policy]    
     ,[Class]    
     ,[Class2]    
     ,[DenialCat]    
     ,[DenialCat2]    
     ,[MaritalStatus]    
     ,[SSN]    
     ,[PregIND]    
     ,[AbsentParent]    
     ,[HICN]    
     ,[PilotCat]    
     ,[PilotClass]    
     ,[PilotDenial]    
     ,[HCREElegibilityIND]    
     ,[HCREDenialCode]    
     ,[OtherInsurer1]    
     ,[OtherPolicy1]    
     ,[OtherInsurer2]    
     ,[OtherPolicy2]    
     ,[OtherInsurer3]    
     ,[OtherPolicy3]    
     ,[GroupIdent]    
     ,[MPI]    
     ,[ELAErrors]    
     ,[Agency]    
     ,[MasterPatientIndex]    
     ,[MemberCertificationDate]    
     ,[ContractNumber]    
     ,[MemberPrimaryCenter]    
     ,[MPCEffectiveDate]    
     ,[MemberNewPrimaryCenter]    
     ,[MNPCEffectiveDate]    
     ,[PCP1]    
     ,[PCP1EffectiveDate]    
     ,[PCP2]    
     ,[PCP2EffectiveDate]    
     ,[NewPCP1]    
     ,[NewPCP1EffectiveDate]    
     ,[NewPCP2]    
     ,[NewPCP2EffectiveDate]    
     ,[CardIdNumber]    
     ,[CardIdDate]    
     ,[ELAIndicator]    
     ,[PrimaryCenterPCPChangeReason]    
     ,[MedicaidIndicator]    
     ,[MedicareIndicator]    
     ,[Carrier]    
     ,[CarrierEffectiveDate]    
     ,[NewCarrier]    
     ,[NewCarrierEffectiveDate]    
     ,[PlanType]    
     ,[PlanTypeEffectiveDate]    
     ,[PlanVersion]    
     ,[PlanVersionEffectiveDate]    
     ,[NewPlanType]    
     ,[NewPlanTypeEffectiveDate]    
     ,[NewPlanVersion]    
     ,[NewPlanVersionEffectiveDate]    
     ,[InstitutionalStatus]    
     ,[HICNumberMA]    
     ,[AutoEnrollIndicator]    
     ,[AutoEnrollDate]    
     ,[IPAEspecial]    
     ,[CMSCertStatus]    
     ,[CoverageCode]    
     ,[NewContractNumber]    
     ,[SpecialEnroll]    
     ,[CostSharingFlag]    
     ,[MaxCoPay]    
     ,[ExtensionFlag]    
     ,[SpendDownFlag]    
     ,[GroupCode]    
     ,[BlockNumber]    
     ,[ProcessHeaderId]    
     ,[IsValidForImport]    
  FROM [ExportAses].[MembersRecords]    
  WHERE ProcessHeaderId = @MemberProcessId    
  AND [IsValidForImport] = 1    
    
  INSERT INTO #ASESUpdateData    
                ([Index]    
                ,ProcessHeaderId                   
                ,MPI                    
                ,MCOId    
                ,MCOEffectiveDate    
                ,PMGId    
                ,PMGEffectiveDate    
                ,PCPId    
                ,PCPEffectiveDate    
                )    
   SELECT   AsesUpdateData.[Index]    
     ,AsesUpdateData.ProcessHeaderId         
     ,AsesUpdateData.MPI         
     ,AsesUpdateData.MCOId    
     ,CASE WHEN AsesUpdateData.CarrierID IS NULL THEN NULL ELSE COALESCE(AsesUpdateData.CarrierEffDate,'2018-11-01') END MCOEffectiveDate    
     ,AsesUpdateData.PMGId    
     ,CASE WHEN AsesUpdateData.PmgTaxId IS NULL THEN NULL ELSE COALESCE(AsesUpdateData.PmgEffDate, '2018-11-01') END PMGEffectiveDate    
     ,AsesUpdateData.PCPId    
     ,CASE WHEN AsesUpdateData.PcpNpi IS NULL THEN NULL ELSE COALESCE(AsesUpdateData.PcpEffDate,'2018-11-01') END PCPEffectiveDate              
   FROM    ( SELECT     memdata.[BlockNumber] [Index]    
        ,COALESCE(famdata.ProcessHeaderId, memdata.ProcessHeaderId) ProcessHeaderId            
        ,memdata.MPI    
        ,memdata.CarrierID    
        ,mcos.Id MCOId    
        ,memdata.CarrierEffDate    
        ,pmgs.Id PMGId    
        ,famdata.PmgTaxId    
        ,famdata.PmgEffDate    
        ,pcps.Id PCPId    
        ,memdata.PcpNpi    
        ,memdata.PcpEffDate                    
      FROM      ( SELECT      NULLIF(RTRIM(LTRIM(fam.[PMGTaxId])),'') PmgTaxId    
            ,TRY_CAST(RTRIM(LTRIM(fam.[PMGEffectiveDate])) AS DATE) PmgEffDate    
            ,BlockNumber    
            ,fam.ProcessHeaderId    
            ,fam.[FamilySSN]    
         FROM     #ParsedFamilyData fam    
         WHERE    fam.TranId = 'E' AND ProcessHeaderId = @FamilyProcessId    
        ) famdata    
        INNER JOIN ( SELECT [LastName1]    
             ,[LastName2]    
             ,[FirstName]    
             ,[MPI] MPI    
             ,NULLIF(RTRIM(LTRIM(mem.[Carrier])),'') CarrierID    
             ,TRY_CAST(mem.[CarrierEffectiveDate] AS DATE) CarrierEffDate    
             ,NULLIF(RTRIM(LTRIM([PCP1])),'') PcpNpi    
             ,TRY_CAST(mem.[PCP1EffectiveDate] AS DATE) PcpEffDate    
             ,[ContractNumber]    
             ,BlockNumber      
             ,mem.[FamilySSN]    
             ,mem.[ProcessHeaderId]    
            FROM  #ParsedMemberData mem    
            WHERE mem.TranId = 'E'        
            and ProcessHeaderId = @MemberProcessId      
           ) memdata ON --memdata.[Index] = famdata.[Index] AND     
              memdata.BlockNumber = famdata.BlockNumber    
              AND famdata.FamilySsn = memdata.FamilySsn    
        CROSS APPLY ( SELECT   TOP 1 mco.Id    
            FROM      [Enrollment].[ManagedCareOrganizations] mco    
            WHERE     mco.CarrierCode = memdata.CarrierID    
           ) mcos    
        CROSS APPLY ( SELECT   TOP 1 pmg.Id    
            FROM      [Enrollment].PrimaryMedicalGroup pmg    
            WHERE     pmg.PmgFederalTaxId = famdata.PmgTaxId    
           ) pmgs    
        CROSS APPLY ( SELECT   TOP 1 pcpPerson.Id -- pcp.Id    
            FROM   --   [Enrollment].PrimaryCarePhysician pcp INNER JOIN 
			Enrollment.PersonPrimaryCarePhysician pcpPerson    
            --on pcp.PersonPrimaryCarePhysicianId = pcpPerson.Id    
            WHERE    pcpPerson.NPI = memdata.PcpNpi    
           ) pcps    
     ) AsesUpdateData;    
      
  --Merge data dentro de Family     
  MERGE [Enrollment].[Families] AS target      
  USING (    
   SELECT DISTINCT     
   [FamilyCode],     
   [ApplicationNumber],     
   [SSN],     
   [Suffix],     
   [ContactFirstLastName],     
   [ContactSecondLastName],     
   [ContactFirstName],     
   [ContactMiddleName],     
   [Region],     
   [Facility],     
   [EffectiveDate],     
   [ExpirationDate],     
   [CertificationDate],     
   [MailAddressLineOne],     
   [MailAddressLineTwo],     
   [MailAddressCity],     
   [MailAddressZipCode],     
   [MailAddressZip4],     
   [ResidenceAddressLineOne],     
   [ResidenceAddressLineTwo],     
   [ResidenceAddressCity],     
   [ResidenceAddressZipCode],     
   [ResidenceAddressZip4],     
   [Phone],     
   [TranId]    
   FROM    
   (    
    SELECT ROW_NUMBER() OVER (PARTITION BY [FamilyId],[FamilySSN],[TranId],[ApplicationNumber]  ORDER BY [FamilyId],[FamilySSN],[TranId],[ApplicationNumber]  DESC) AS r,     
    [FamilyId] AS FamilyCode,    
    [ApplicationNumber],    
    [FamilySSN] AS SSN,    
    [FamilySuffix] AS Suffix,    
    [ContactLastName1] AS ContactFirstLastName,    
    [ContactLastName2] AS ContactSecondLastName,    
    [ContactFirstName],    
    NULL AS ContactMiddleName,    
    [Region],    
    [Facility],    
    CASE     
     WHEN LEN([EffectiveDate]) >= 8    
      THEN ISNULL(NULLIF(PARSE(CONCAT(SUBSTRING([EffectiveDate],1,2),'/', SUBSTRING([EffectiveDate],3,2), '/', SUBSTRING([EffectiveDate],5,4)) As date  USING 'en-US'), ''),NULL)       
     ELSE NULL    
    END AS [EffectiveDate],     
    NULL AS [ExpirationDate],    
    CASE     
     WHEN LEN([CertificationDate]) >= 8    
      THEN ISNULL(NULLIF(PARSE(CONCAT(SUBSTRING([CertificationDate],1,2),'/', SUBSTRING([CertificationDate],3,2), '/', SUBSTRING([CertificationDate],5,4)) As date  USING 'en-US'), ''),NULL)       
     ELSE NULL    
    END AS [CertificationDate],    
    [MailingAddress1] AS [MailAddressLineOne],    
    [MailingAddress2] AS [MailAddressLineTwo],    
    [MailingCity] AS [MailAddressCity],    
    [MailingZip] AS [MailAddressZipCode],    
    [MalingZip4] AS [MailAddressZip4],    
    [ResidenceAddress1] AS [ResidenceAddressLineOne],    
    [ResidenceAddress2] AS [ResidenceAddressLineTwo],    
    [ResidenceCity] AS [ResidenceAddressCity],    
    [ResidenceZip] AS [ResidenceAddressZipCode],    
    [ResidenceZip4] AS [ResidenceAddressZip4],    
    [Phone],    
    [TranId]    
    FROM #ParsedFamilyData    
    ) AS r    
   WHERE r.r = 1    
  )      
  AS SOURCE ([FamilyCode], [ApplicationNumber], [SSN], [Suffix], [ContactFirstLastName], [ContactSecondLastName], [ContactFirstName], [ContactMiddleName], [Region], [Facility], [EffectiveDate], [ExpirationDate], [CertificationDate], [MailAddressLineOne], 
 
 [MailAddressLineTwo], [MailAddressCity], [MailAddressZipCode], [MailAddressZip4], [ResidenceAddressLineOne], [ResidenceAddressLineTwo], [ResidenceAddressCity], [ResidenceAddressZipCode], [ResidenceAddressZip4], [Phone], [TranId])    
  ON     
  (ISNULL(target.[FamilyCode],'')  = ISNULL(source.[FamilyCode],'')     
   AND ISNULL(target.[SSN],'') = ISNULL(source.[SSN],'')    
   AND ISNULL(target.[TranId],'') = ISNULL(source.[TranId],'')    
   AND ISNULL(target.[ApplicationNumber],'') = ISNULL(source.[ApplicationNumber],'')    
  )    
  WHEN MATCHED THEN       
   UPDATE SET [FamilyCode] = source.[FamilyCode],      
        [ApplicationNumber] = source.[ApplicationNumber],      
        [SSN] = source.[SSN],      
        [Suffix] = source.[Suffix],    
        [ContactFirstLastName] = source.[ContactFirstLastName],    
        [ContactSecondLastName] = source.[ContactSecondLastName],    
        [ContactFirstName] = source.[ContactFirstName],    
        [ContactMiddleName] = source.[ContactMiddleName],    
        [Region] = source.[Region],    
        [Facility] = source.[Facility],    
        [EffectiveDate] = source.[EffectiveDate],    
        [ExpirationDate] = source.[ExpirationDate],    
        [CertificationDate] = source.[CertificationDate],    
        [MailAddressLineOne] = source.[MailAddressLineOne],    
        [MailAddressLineTwo] = source.[MailAddressLineTwo],    
        [MailAddressCity] = source.[MailAddressCity],    
        [MailAddressZipCode] = source.[MailAddressZipCode],    
        [ResidenceAddressLineOne] = source.[ResidenceAddressLineOne],    
        [ResidenceAddressLineTwo] = source.[ResidenceAddressLineTwo],    
        [ResidenceAddressCity] = source.[ResidenceAddressCity],    
        [ResidenceAddressZipCode] = source.[ResidenceAddressZipCode],    
        [Phone] = source.[Phone],    
        [TranId] = source.[TranId],    
        [UpdatedBy] = @UserName,    
        [UpdatedOn] = GETDATE()    
  WHEN NOT MATCHED BY TARGET THEN     
  INSERT ([FamilyCode],[ApplicationNumber],[SSN],[Suffix],[ContactFirstLastName],[ContactSecondLastName],[ContactFirstName],[ContactMiddleName],[Region],[Facility],[EffectiveDate],[ExpirationDate],[CertificationDate],[MailAddressLineOne],[MailAddressLineTwo],[MailAddressCity],[MailAddressZipCode], [MailAddressZip4],[ResidenceAddressLineOne], [ResidenceAddressLineTwo], [ResidenceAddressCity], [ResidenceAddressZipCode],[ResidenceAddressZip4],[Phone],[TranId], [CreatedBy],[CreatedOn],[Enabled])    
  VALUES ([FamilyCode],[ApplicationNumber],[SSN],[Suffix],[ContactFirstLastName],[ContactSecondLastName],[ContactFirstName],[ContactMiddleName],[Region],[Facility],[EffectiveDate],[ExpirationDate],[CertificationDate],[MailAddressLineOne],[MailAddressLineTwo],[MailAddressCity],[MailAddressZipCode], [MailAddressZip4],[ResidenceAddressLineOne], [ResidenceAddressLineTwo], [ResidenceAddressCity], [ResidenceAddressZipCode],[ResidenceAddressZip4],[Phone],[TranId], @UserName, GETDATE(), 1);    
      
  --Merge data dentro de Member    
  MERGE [Enrollment].[Members] AS target      
  USING (    
   SELECT DISTINCT    
   [FamilyId],    
   SSN,    
   RIGHT(SSN,4) Last4SSN,    
   Suffix,    
   [MPI],    
   [MPIshort],    
   [MPIContactMember],    
   [FirstLastName],    
   [SecondLastName],    
   [FirstName],    
   [MiddleName],    
   [DateOfBirth],    
   [CertificationDate],     
   [ExpirationDate],    
   [PlanType],    
   [PlanVersion],    
   [MemberPrimaryCenter],    
   [MedicaidIndicator],    
   [MedicareIndicator],    
   [HICNumber],    
   GenderId,
   [Coveragecode],  
   [TranId]       
   FROM    
   (         
    SELECT ROW_NUMBER() OVER (PARTITION BY SSN ORDER BY SSN DESC) AS r,      
    (SELECT TOP 1 Id from [Enrollment].[Families] WHERE SSN = #ParsedMemberData.[FamilySSN]) as [FamilyId],       
    [SSN] AS SSN,    
    [FamilySuffix] AS Suffix,    
    [MasterPatientIndex] as [MPI],    
    [MPI] as [MPIshort],    
    [ContactMember] as [MPIContactMember],    
    [LastName1] AS [FirstLastName],    
    [LastName2] AS [SecondLastName],    
    [FirstName],    
    [MiddleName],    
    CASE     
     WHEN LEN([DateOfBirth]) >= 8    
      THEN ISNULL(NULLIF(PARSE(CONCAT(SUBSTRING([DateOfBirth],1,2),'/', SUBSTRING([DateOfBirth],3,2), '/', SUBSTRING([DateOfBirth],5,4)) As date  USING 'en-US'), ''),NULL)       
     ELSE NULL    
    END AS [DateOfBirth],    
    CASE     
     WHEN LEN([MemberCertificationDate]) >= 8    
      THEN ISNULL(NULLIF(PARSE(CONCAT(SUBSTRING([MemberCertificationDate],1,2),'/', SUBSTRING([MemberCertificationDate],3,2), '/', SUBSTRING([MemberCertificationDate],5,4)) As date  USING 'en-US'), ''),NULL)       
     ELSE NULL    
    END AS [CertificationDate],       
    NULL AS [ExpirationDate],    
    [PlanType],    
    [PlanVersion],    
    [MemberPrimaryCenter],    
    [MedicaidIndicator],    
    [MedicareIndicator],    
    [HICNumberMA] AS [HICNumber],    
	COALESCE((SELECT Id FROM Enrollment.Genders WHERE Code=[Sex]), 3) GenderId,
    [Coveragecode],  
    [TranId]    
    FROM #ParsedMemberData    
    ) AS r    
   WHERE r.r = 1    
  )      
  AS source (FamilyId, [SSN], Last4SSN, [Suffix], [MPI], [MPIshort], [MPIContactMember], [FirstLastName], [SecondLastName], [FirstName], [MiddleName]    
      ,[DateOfBirth], [CertificationDate], [ExpirationDate], [PlanType], [PlanVersion], [MemberPrimaryCenter], [MedicaidIndicator]    
      ,[MedicareIndicator],[HICNumber],[GenderId],[Coveragecode], [TranId])    
  ON (ISNULL(target.[FamilyId],'')  = ISNULL(source.FamilyId,'') AND ISNULL(target.[SSN],'') = ISNULL(source.[SSN],''))    
  WHEN MATCHED THEN       
   UPDATE SET FamilyId = source.FamilyId,      
        [SSN] = source.[SSN],       
        Last4SSN = source.Last4SSN,    
        [Suffix] = source.[Suffix],    
        [MPI] = source.[MPI],    
        [MPIshort] = source.[MPIshort],    
        [MPIContactMember] = source.[MPIContactMember],    
        [FirstLastName] = source.[FirstLastName],    
        [SecondLastName] = source.[SecondLastName],    
        [FirstName] = source.[FirstName],    
        [MiddleName] = source.[MiddleName],    
        [DateOfBirth] = source.[DateOfBirth],    
        [CertificationDate] = source.[CertificationDate],    
        [ExpirationDate] = source.[ExpirationDate],    
        [PlanType] = source.[PlanType],    
        [PlanVersion] = source.[PlanVersion],    
        [MemberPrimaryCenter] = source.[MemberPrimaryCenter],    
        [MedicaidIndicator] = source.[MedicaidIndicator],    
        [MedicareIndicator] = source.[MedicareIndicator],    
        [HICNumber] = source.[HICNumber],   
		[GenderId] = source.[GenderId],  
        [Coveragecode] = source.[Coveragecode],   
        [TranId] = source.[TranId],    
        [UpdatedBy] = @UserName,    
        [UpdatedOn] = GETDATE()    
  WHEN NOT MATCHED BY TARGET THEN     
  INSERT  ([FamilyId],[SSN],Last4SSN, [Suffix],[MPI],[MPIshort],[MPIContactMember],[FirstLastName],[SecondLastName],[FirstName],[MiddleName]    
      ,[DateOfBirth],[CertificationDate],[ExpirationDate],[PlanType],[PlanVersion],[MemberPrimaryCenter],[MedicaidIndicator]    
      ,[MedicareIndicator],[HICNumber],[GenderId],[Coveragecode],[TranId],[CreatedBy],[CreatedOn],[Enabled])    
  VALUES  ([FamilyId],[SSN],Last4SSN, [Suffix],[MPI],[MPIshort],[MPIContactMember],[FirstLastName],[SecondLastName],[FirstName],[MiddleName]    
      ,[DateOfBirth],[CertificationDate],[ExpirationDate],[PlanType],[PlanVersion],[MemberPrimaryCenter],[MedicaidIndicator]    
      ,[MedicareIndicator],[HICNumber],[GenderId],[Coveragecode],[TranId], @UserName, GETDATE(), 1);      
      
  DECLARE crsrUpdateMcos CURSOR FAST_FORWARD    
  FOR    
   SELECT McoId, MCOEffectiveDate, PMGId, PMGEffectiveDate, PCPId, PCPEffectiveDate, MPI    
   FROM    #ASESUpdateData    
   ORDER BY [Index];    
    
  --Actualizando MCO-PMG-PCP    
  OPEN crsrUpdateMcos;      
  FETCH NEXT FROM crsrUpdateMcos INTO @McoId, @McoEffectiveDate, @PmgId, @PmgEffectiveDate, @PcpId, @PcpEffectiveDate,  @MPI;    
  WHILE @@FETCH_STATUS = 0    
   BEGIN      
    --Actualizando MCO-PMG-PCP    
    UPDATE Member    
       SET     
        [MCOId] = @McoId    
       ,[MCOEffectiveDate] = @McoEffectiveDate    
       ,[PMGId] = @PmgId    
       ,[PMGEffectiveDate] = @PmgEffectiveDate    
       ,[PCPId] = @PcpId    
       ,[PCPEffectiveDate] = @PcpEffectiveDate    
       ,[UpdatedBy] = @UserName    
       ,[UpdatedOn] = GETDATE()    
       ,[Enabled] = 1    
     FROM [Enrollment].[Members] Member    
     WHERE [MPIshort] = @MPI    
        
    ----Registrando en historico        
    --INSERT INTO [Enrollment].[EnrollmentHistories]    
    --     ([MemberId],[FamilyId],[SSN],[Suffix],[MPI],[MPIshort],[MPIContactMember]    
    --     ,[FirstLastName],[SecondLastName],[FirstName],[MiddleName],[DateOfBirth],[CertificationDate],[PlanType]    
    --     ,[PlanVersion],[MemberPrimaryCenter],[MedicaidIndicator],[MedicareIndicator],[HICNumber]    
    --     ,[MCOId],[MCOModifiedSource],[MCOModifiedBy],[MCOModifiedDate],[MCOEffectiveDate]    
    --     ,[PMGId],[PMGModifiedSource],[PMGModifiedBy],[PMGModifiedDate],[PMGEffectiveDate]    
    --     ,[PCPId],[PCPModifiedSource],[PCPModifiedBy],[PCPModifiedDate],[PCPEffectiveDate]    
    --     ,[TranId],[CreatedBy],[CreatedOn],[Enabled])    
    --SELECT TOP 1     
    --   [Id],[FamilyId],[SSN],[Suffix],[MPI],[MPIshort],[MPIContactMember]    
    --     ,[FirstLastName],[SecondLastName],[FirstName],[MiddleName],[DateOfBirth],[CertificationDate],[PlanType]    
    --     ,[PlanVersion],[MemberPrimaryCenter],[MedicaidIndicator],[MedicareIndicator],[HICNumber]    
    --     ,@McoId AS [MCOId]    
    --     ,'ASES' AS [MCOModifiedSource]    
    --     ,'ASES' AS [MCOModifiedBy]    
    --     ,GETDATE() AS [MCOModifiedDate]    
    --     ,@McoEffectiveDate AS [MCOEffectiveDate]    
    --     ,@PmgId AS [PMGId]    
    --     ,'ASES' AS [PMGModifiedSource]    
    --     ,'ASES' AS [PMGModifiedBy]    
    --     ,GETDATE() AS [PMGModifiedDate]    
    --     ,@PmgEffectiveDate AS [PMGEffectiveDate]    
    --     ,@PcpId AS [PCPId]    
    --     ,'ASES' AS [PCPModifiedSource]    
    --     ,'ASES' AS [PCPModifiedBy]    
    --     ,GETDATE() AS [PCPModifiedDate]    
    --     ,@PcpEffectiveDate AS [PCPEffectiveDate]    
    --     ,[TranId]    
    --     ,@UserName AS [CreatedBy]    
    --     ,GETDATE() AS [CreatedOn]    
    --     ,1 AS [Enabled]    
    --FROM [Enrollment].[Members]     
    --WHERE [MPIshort] = @MPI    
        
    FETCH NEXT FROM crsrUpdateMcos INTO @McoId, @McoEffectiveDate, @PmgId, @PmgEffectiveDate, @PcpId, @PcpEffectiveDate,  @MPI;    
  END;     
    
  CLOSE crsrUpdateMcos;      
  DEALLOCATE crsrUpdateMcos;    
    
   --select 6/0 from dual
         
  SET @Error = @@ERROR      
      
  if @trancount = 0     
   commit;    
    
  SELECT    
   @ProcessType AS ProcessType    
  ,@Error AS ErrorNumber    
  ,@Procedure AS ProcedureName    
  ,NULL AS ErrorLine    
  ,NULL AS ProcessMessage      
 END TRY      
 BEGIN CATCH    
     DECLARE @xstate int;    
        select @xstate = XACT_STATE();    
  SELECT    
   @ProcessType AS ProcessType    
  ,CAST(ERROR_NUMBER() AS VARCHAR) AS ErrorNumber    
  ,ERROR_PROCEDURE() AS ProcedureName    
  ,CAST(ERROR_LINE() AS VARCHAR) AS ErrorLine    
  ,ERROR_MESSAGE() AS ProcessMessage    
    
  if @xstate = -1    
            rollback;    
        if @xstate = 1 and @trancount = 0    
            rollback    
        if @xstate = 1 and @trancount > 0    
            rollback transaction @TransactionName;    
    
      
 END CATCH    
END;