﻿CREATE TABLE [ExportAses].[MembersRecords] (
    [TranId]                       NCHAR (2)      COLLATE Modern_Spanish_CI_AS NULL,
    [ProcessDate]                  NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [FamilySSN]                    NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [FamilySuffix]                 NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [MemberSSN]                    NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [MemberSuffix]                 NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [ContactMember]                NVARCHAR (11)  COLLATE Modern_Spanish_CI_AS NULL,
    [LastName1]                    NVARCHAR (15)  COLLATE Modern_Spanish_CI_AS NULL,
    [LastName2]                    NVARCHAR (15)  COLLATE Modern_Spanish_CI_AS NULL,
    [FirstName]                    NVARCHAR (20)  COLLATE Modern_Spanish_CI_AS NULL,
    [MiddleName]                   NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [Relationship]                 NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [DateOfBirth]                  NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [PlaceOfBirth]                 NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [Sex]                          NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [Category]                     NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [Category2]                    NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [Condition]                    NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [SourceCode]                   NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [ReceiveSS]                    NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [MedInsCode]                   NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [Policy]                       NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [Class]                        NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [Class2]                       NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [DenialCat]                    NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [DenialCat2]                   NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [MaritalStatus]                NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [SSN]                          NVARCHAR (9)   COLLATE Modern_Spanish_CI_AS NULL,
    [PregIND]                      NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [AbsentParent]                 NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [HICN]                         NVARCHAR (11)  COLLATE Modern_Spanish_CI_AS NULL,
    [PilotCat]                     NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [PilotClass]                   NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [PilotDenial]                  NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [HCREElegibilityIND]           NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [HCREDenialCode]               NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [OtherInsurer1]                NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [OtherPolicy1]                 NVARCHAR (20)  COLLATE Modern_Spanish_CI_AS NULL,
    [OtherInsurer2]                NCHAR (10)     COLLATE Modern_Spanish_CI_AS NULL,
    [OtherPolicy2]                 NVARCHAR (20)  COLLATE Modern_Spanish_CI_AS NULL,
    [OtherInsurer3]                NCHAR (10)     COLLATE Modern_Spanish_CI_AS NULL,
    [OtherPolicy3]                 NVARCHAR (20)  COLLATE Modern_Spanish_CI_AS NULL,
    [GroupIdent]                   NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [MPI]                          NVARCHAR (11)  COLLATE Modern_Spanish_CI_AS NULL,
    [ELAErrors]                    NVARCHAR (20)  COLLATE Modern_Spanish_CI_AS NULL,
    [Agency]                       NVARCHAR (5)   COLLATE Modern_Spanish_CI_AS NULL,
    [MasterPatientIndex]           NVARCHAR (13)  COLLATE Modern_Spanish_CI_AS NULL,
    [MemberCertificationDate]      NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [ContractNumber]               NVARCHAR (13)  COLLATE Modern_Spanish_CI_AS NULL,
    [MemberPrimaryCenter]          NVARCHAR (4)   COLLATE Modern_Spanish_CI_AS NULL,
    [MPCEffectiveDate]             NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [MemberNewPrimaryCenter]       NVARCHAR (4)   COLLATE Modern_Spanish_CI_AS NULL,
    [MNPCEffectiveDate]            NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [PCP1]                         NVARCHAR (15)  COLLATE Modern_Spanish_CI_AS NULL,
    [PCP1EffectiveDate]            NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [PCP2]                         NVARCHAR (15)  COLLATE Modern_Spanish_CI_AS NULL,
    [PCP2EffectiveDate]            NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [NewPCP1]                      NVARCHAR (15)  COLLATE Modern_Spanish_CI_AS NULL,
    [NewPCP1EffectiveDate]         NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [NewPCP2]                      NVARCHAR (15)  COLLATE Modern_Spanish_CI_AS NULL,
    [NewPCP2EffectiveDate]         NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [CardIdNumber]                 NVARCHAR (15)  COLLATE Modern_Spanish_CI_AS NULL,
    [CardIdDate]                   NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [ELAIndicator]                 NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [PrimaryCenterPCPChangeReason] NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [MedicaidIndicator]            NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [MedicareIndicator]            NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [Carrier]                      NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [CarrierEffectiveDate]         NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [NewCarrier]                   NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [NewCarrierEffectiveDate]      NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [PlanType]                     NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [PlanTypeEffectiveDate]        NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [PlanVersion]                  NVARCHAR (3)   COLLATE Modern_Spanish_CI_AS NULL,
    [PlanVersionEffectiveDate]     NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [NewPlanType]                  NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [NewPlanTypeEffectiveDate]     NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [NewPlanVersion]               NVARCHAR (3)   COLLATE Modern_Spanish_CI_AS NULL,
    [NewPlanVersionEffectiveDate]  NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [InstitutionalStatus]          NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [HICNumberMA]                  NVARCHAR (12)  COLLATE Modern_Spanish_CI_AS NULL,
    [AutoEnrollIndicator]          NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [AutoEnrollDate]               NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [IPAEspecial]                  NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [CMSCertStatus]                NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [CoverageCode]                 NVARCHAR (3)   COLLATE Modern_Spanish_CI_AS NULL,
    [NewContractNumber]            NVARCHAR (13)  COLLATE Modern_Spanish_CI_AS NULL,
    [SpecialEnroll]                NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [CostSharingFlag]              NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [MaxCoPay]                     NVARCHAR (5)   COLLATE Modern_Spanish_CI_AS NULL,
    [ExtensionFlag]                NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [SpendDownFlag]                NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [GroupCode]                    NVARCHAR (3)   COLLATE Modern_Spanish_CI_AS NULL,
    [BlockNumber]                  NCHAR (6)      COLLATE Modern_Spanish_CI_AS NULL,
    [ProcessHeaderId]              INT            NULL,
    [IsValidForImport]             BIT            NULL,
    [MessageInvalid]               NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Deceased_Date]                NVARCHAR (20)  NULL,
    CONSTRAINT [FK_MembersRecords_ProcessHeader] FOREIGN KEY ([ProcessHeaderId]) REFERENCES [ExportAses].[ProcessHeader] ([ID])
);







