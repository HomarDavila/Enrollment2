﻿CREATE TABLE [Enrollment].[EnrollmentHistory] (
    [Id]                  INT            NOT NULL,
    [MemberId]            INT            NOT NULL,
    [FamilyId]            INT            NOT NULL,
    [SSN]                 NVARCHAR (9)   NULL,
    [Suffix]              NVARCHAR (2)   NULL,
    [MPI]                 NVARCHAR (13)  NULL,
    [MPIshort]            NVARCHAR (11)  NULL,
    [MPIContactMember]    NVARCHAR (11)  NULL,
    [FirstLastName]       NVARCHAR (50)  NULL,
    [SecondLastName]      NVARCHAR (50)  NULL,
    [FirstName]           NVARCHAR (100) NOT NULL,
    [MiddleName]          NVARCHAR (50)  NULL,
    [DateOfBirth]         DATE           NULL,
    [CertificationDate]   DATE           NULL,
    [PlanType]            NVARCHAR (2)   NULL,
    [PlanVersion]         NVARCHAR (2)   NULL,
    [MemberPrimaryCenter] NVARCHAR (4)   NULL,
    [MedicaidIndicator]   NVARCHAR (2)   NULL,
    [MedicareIndicator]   NVARCHAR (2)   NULL,
    [HICNumber]           NVARCHAR (12)  NULL,
    [MCOId]               INT            NULL,
    [MCOEffectiveDate]    DATE           NULL,
    [PMGId]               INT            NULL,
    [PMGEffectiveDate]    DATE           NULL,
    [PCPId]               INT            NULL,
    [PCPEffectiveDate]    DATE           NULL,
    [TranId]              NVARCHAR (2)   NULL,
    [CreatedBy]           NVARCHAR (100) NOT NULL,
    [CreatedOn]           DATETIME       NOT NULL,
    [UpdatedBy]           NVARCHAR (100) NULL,
    [UpdatedOn]           DATETIME       NULL,
    [Enabled]             BIT            NOT NULL,
    CONSTRAINT [PK_EnrollmentHistory] PRIMARY KEY CLUSTERED ([Id] ASC)
);
