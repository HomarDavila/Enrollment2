CREATE TABLE [Enrollment].[EnrollmentHistories] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [MemberId]            INT            NOT NULL,
    [FamilyId]            INT            NOT NULL,
    [SSN]                 NVARCHAR (9)   NULL,
    [Suffix]              NVARCHAR (2)   NULL,
    [MPI]                 NVARCHAR (13)  NULL,
    [MPIshort]            NVARCHAR (11)  NULL,
    [MPIContactMember]    NVARCHAR (11)  NULL,
    [FirstLastName]       NVARCHAR (50)  NULL,
    [SecondLastName]      NVARCHAR (50)  NULL,
    [FirstName]           NVARCHAR (100) NULL,
    [MiddleName]          NVARCHAR (50)  NULL,
    [DateOfBirth]         DATE           NULL,
    [CertificationDate]   DATE           NULL,
    [PlanType]            NVARCHAR (2)   NULL,
    [PlanVersion]         NVARCHAR (2)   NULL,
    [MemberPrimaryCenter] NVARCHAR (10)  NULL,
    [MedicaidIndicator]   NVARCHAR (2)   NULL,
    [MedicareIndicator]   NVARCHAR (2)   NULL,
    [HICNumber]           NVARCHAR (12)  NULL,
    [MCOId]               INT            NULL,
    [MCOModifiedSource]   NVARCHAR (100) NULL,
    [MCOModifiedBy]       NVARCHAR (100) NULL,
    [MCOModifiedDate]     DATE           NULL,
    [MCOEffectiveDate]    DATE           NULL,
    [PMGId]               INT            NULL,
    [PMGModifiedSource]   NVARCHAR (100) NULL,
    [PMGModifiedBy]       NVARCHAR (100) NULL,
    [PMGModifiedDate]     DATE           NULL,
    [PMGEffectiveDate]    DATE           NULL,
    [PCPId]               INT            NULL,
    [PCPModifiedSource]   NVARCHAR (100) NULL,
    [PCPModifiedBy]       NVARCHAR (100) NULL,
    [PCPModifiedDate]     DATE           NULL,
    [PCPEffectiveDate]    DATE           NULL,
    [TranId]              NVARCHAR (2)   NULL,
    [CreatedBy]           NVARCHAR (100) NULL,
    [CreatedOn]           DATETIME       NOT NULL,
    [UpdatedBy]           NVARCHAR (100) NULL,
    [UpdatedOn]           DATETIME       NULL,
    [Enabled]             BIT            NOT NULL,
    [filePDF]             VARCHAR (250)  NULL,
    [PreviusPmg]          INT            NULL,
    [PreviusPcp]          INT            NULL,
    [PreviusMco]          INT            NULL,
    [ChangeReason]        VARCHAR (1)    NULL,
    [State]               VARCHAR (20)   NULL,
    [JustCauseReasonId]   INT            NULL,
    [StatusId]            INT            NULL,
    [Phone]               VARCHAR (200)  NULL,
    [EstadoEncuesta]      BIT            NULL,
    [MCOChange]           BIT            NULL,
    [PCPChange]           BIT            NULL,
    [PMGChange]           BIT            NULL,
    [JustCauseComment]    NVARCHAR (100) NULL,
    [Email]               VARCHAR (200)  NULL,
    CONSTRAINT [PK_EnrollmentHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EnrollmentHistories_Status] FOREIGN KEY ([StatusId]) REFERENCES [Enrollment].[Status] ([Id])
);

















