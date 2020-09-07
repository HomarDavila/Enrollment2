CREATE TABLE [Enrollment].[Members] (
    [Id]                        INT            IDENTITY (1, 1) NOT NULL,
    [FamilyId]                  INT            NULL,
    [SSN]                       NVARCHAR (9)   NULL,
    [Last4SSN]                  NVARCHAR (4)   COLLATE Modern_Spanish_CI_AS NULL,
    [Suffix]                    NVARCHAR (2)   NULL,
    [MPI]                       NVARCHAR (13)  NULL,
    [MPIshort]                  NVARCHAR (11)  NULL,
    [MPIContactMember]          NVARCHAR (11)  NULL,
    [FirstLastName]             NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [SecondLastName]            NVARCHAR (50)  COLLATE Modern_Spanish_CI_AS NULL,
    [FirstName]                 NVARCHAR (100) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [MiddleName]                NVARCHAR (50)  NULL,
    [DateOfBirth]               DATE           NULL,
    [CertificationDate]         DATE           NULL,
    [ExpirationDate]            DATE           NULL,
    [PlanType]                  NVARCHAR (2)   NULL,
    [PlanVersion]               NVARCHAR (2)   NULL,
    [MemberPrimaryCenter]       NVARCHAR (10)  NULL,
    [MedicaidIndicator]         NVARCHAR (2)   NULL,
    [MedicareIndicator]         NVARCHAR (2)   NULL,
    [HICNumber]                 NVARCHAR (12)  NULL,
    [MCOId]                     INT            NULL,
    [MCOEffectiveDate]          DATE           NULL,
    [PMGId]                     INT            NULL,
    [PMGEffectiveDate]          DATE           NULL,
    [PCPId]                     INT            NULL,
    [PCPEffectiveDate]          DATE           NULL,
    [TranId]                    NVARCHAR (2)   NULL,
    [CreatedBy]                 NVARCHAR (100) NOT NULL,
    [CreatedOn]                 DATETIME       NOT NULL,
    [UpdatedBy]                 NVARCHAR (100) NULL,
    [UpdatedOn]                 DATETIME       NULL,
    [Enabled]                   BIT            NOT NULL,
    [FlagM]                     NVARCHAR (50)  NULL,
    [FlagO]                     NVARCHAR (50)  NULL,
    [NewCertificationDate]      DATE           NULL,
    [Coveragecode]              NCHAR (3)      NULL,
    [GenderId]                  INT            NULL,
    [OriginalCertificationDate] DATE           NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([GenderId]) REFERENCES [Enrollment].[Genders] ([Id]),
    FOREIGN KEY ([PCPId]) REFERENCES [Enrollment].[PersonPrimaryCarePhysician] ([Id]),
    CONSTRAINT [FK_Members_Families] FOREIGN KEY ([FamilyId]) REFERENCES [Enrollment].[Families] ([Id]),
    CONSTRAINT [FK_Members_ManagedCareOrganizations] FOREIGN KEY ([MCOId]) REFERENCES [Enrollment].[ManagedCareOrganizations] ([Id]),
    CONSTRAINT [FK_Members_PrimaryMedicalGroup] FOREIGN KEY ([PMGId]) REFERENCES [Enrollment].[PrimaryMedicalGroup] ([Id])
);
















GO
CREATE NONCLUSTERED INDEX [I_SecondLastName]
    ON [Enrollment].[Members]([SecondLastName] ASC);


GO
CREATE NONCLUSTERED INDEX [I_Last4SSN]
    ON [Enrollment].[Members]([Last4SSN] ASC);


GO
CREATE NONCLUSTERED INDEX [I_FirstLastName]
    ON [Enrollment].[Members]([FirstLastName] ASC);


GO
CREATE NONCLUSTERED INDEX [I_DateOfBirth]
    ON [Enrollment].[Members]([DateOfBirth] ASC);


GO
CREATE NONCLUSTERED INDEX [I_MPIshort]
    ON [Enrollment].[Members]([MPIshort] ASC);


GO
CREATE NONCLUSTERED INDEX [I_FirstLastName_FirstName]
    ON [Enrollment].[Members]([FirstLastName] ASC, [FirstName] ASC)
    INCLUDE([FamilyId], [SSN], [Last4SSN], [Suffix], [MPI], [MPIshort], [MPIContactMember], [SecondLastName], [MiddleName], [DateOfBirth], [CertificationDate], [ExpirationDate], [PlanType], [PlanVersion], [MemberPrimaryCenter], [MedicaidIndicator], [MedicareIndicator], [HICNumber], [MCOId], [MCOEffectiveDate], [PMGId], [PMGEffectiveDate], [PCPId], [PCPEffectiveDate], [TranId], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [Enabled], [NewCertificationDate], [GenderId]);

