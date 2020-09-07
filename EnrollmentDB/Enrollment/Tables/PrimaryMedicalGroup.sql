CREATE TABLE [Enrollment].[PrimaryMedicalGroup] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [PmgCode]         NCHAR (10)     NULL,
    [PmgName]         NVARCHAR (80)  NULL,
    [PmgFederalTaxId] NVARCHAR (20)  NULL,
    [NPI]             NVARCHAR (10)  NULL,
    [CreatedBy]       NVARCHAR (100) NULL,
    [CreatedOn]       DATETIME       NOT NULL,
    [UpdatedBy]       NVARCHAR (100) NULL,
    [UpdatedOn]       DATETIME       NULL,
    [Enabled]         BIT            NOT NULL,
    CONSTRAINT [PK_PrimaryMedicalGroup] PRIMARY KEY CLUSTERED ([Id] ASC)
);










GO
CREATE NONCLUSTERED INDEX [IX_PmgName_001]
    ON [Enrollment].[PrimaryMedicalGroup]([PmgName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PmgCode_001]
    ON [Enrollment].[PrimaryMedicalGroup]([PmgCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NPI_001]
    ON [Enrollment].[PrimaryMedicalGroup]([NPI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FederalTaxId_001]
    ON [Enrollment].[PrimaryMedicalGroup]([PmgFederalTaxId] ASC);

