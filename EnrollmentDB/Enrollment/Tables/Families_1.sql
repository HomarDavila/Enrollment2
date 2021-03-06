﻿CREATE TABLE [Enrollment].[Families] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [FamilyCode]              NVARCHAR (11)  NOT NULL,
    [ApplicationNumber]       NVARCHAR (11)  NULL,
    [SSN]                     NVARCHAR (9)   NULL,
    [Suffix]                  NVARCHAR (2)   NULL,
    [ContactFirstLastName]    NVARCHAR (50)  NULL,
    [ContactSecondLastName]   NVARCHAR (50)  NULL,
    [ContactFirstName]        NVARCHAR (100) NULL,
    [ContactMiddleName]       NVARCHAR (50)  NULL,
    [Region]                  NVARCHAR (2)   NULL,
    [Municipality]            NVARCHAR (4)   NULL,
    [Facility]                NVARCHAR (4)   NULL,
    [EffectiveDate]           DATE           NULL,
    [ExpirationDate]          DATE           NULL,
    [CertificationDate]       DATE           NULL,
    [MailAddressLineOne]      NVARCHAR (50)  NULL,
    [MailAddressLineTwo]      NVARCHAR (50)  NULL,
    [MailAddressCity]         NVARCHAR (50)  NULL,
    [MailAddressZipCode]      NVARCHAR (50)  COLLATE Modern_Spanish_CI_AS NULL,
    [MailAddressZip4]         NVARCHAR (10)  NULL,
    [ResidenceAddressLineOne] NVARCHAR (50)  NULL,
    [ResidenceAddressLineTwo] NVARCHAR (50)  NULL,
    [ResidenceAddressCity]    NVARCHAR (50)  NULL,
    [ResidenceAddressZipCode] NVARCHAR (50)  COLLATE Modern_Spanish_CI_AS NULL,
    [ResidenceAddressZip4]    NVARCHAR (10)  NULL,
    [Phone]                   NVARCHAR (20)  NULL,
    [TranId]                  NVARCHAR (2)   NULL,
    [CreatedBy]               NVARCHAR (100) NOT NULL,
    [CreatedOn]               DATETIME       NOT NULL,
    [UpdatedBy]               NVARCHAR (100) NULL,
    [UpdatedOn]               DATETIME       NULL,
    [Enabled]                 BIT            NOT NULL,
    CONSTRAINT [PK_Families] PRIMARY KEY CLUSTERED ([Id] ASC)
);










GO
CREATE NONCLUSTERED INDEX [I_ResidenceAddressZipCode]
    ON [Enrollment].[Families]([ResidenceAddressZipCode] ASC);


GO
CREATE NONCLUSTERED INDEX [I_MailAddressZipCode]
    ON [Enrollment].[Families]([MailAddressZipCode] ASC);

