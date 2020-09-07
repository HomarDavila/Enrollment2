CREATE TABLE [Enrollment].[ManagedCareOrganizations] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [CarrierCode]           CHAR (2)       NULL,
    [CarrierName]           VARCHAR (75)   NULL,
    [LogoURL]               NVARCHAR (200) NULL,
    [Capacity]              INT            NOT NULL,
    [AmountOfLivesEnrolled] INT            NOT NULL,
    [NPI]                   VARCHAR (10)   NULL,
    [EIN]                   VARCHAR (9)    NULL,
    [CreatedBy]             VARCHAR (100)  NULL,
    [CreatedOn]             DATETIME       NOT NULL,
    [UpdatedBy]             VARCHAR (100)  NULL,
    [UpdatedOn]             DATETIME       NULL,
    [Enabled]               BIT            NOT NULL,
    CONSTRAINT [PK_ManagedCareOrganization] PRIMARY KEY CLUSTERED ([Id] ASC)
);









