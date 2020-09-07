CREATE TABLE [Enrollment].[PersonPrimaryCarePhysician] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [FederalTaxId]     NVARCHAR (20)  NULL,
    [NPI]              NVARCHAR (10)  NULL,
    [FullName]         NVARCHAR (170) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [FirstName]        NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [MiddleName]       NVARCHAR (30)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [FirstLastName]    NVARCHAR (30)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [SecondLastName]   NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [CreatedBy]        NVARCHAR (100) NULL,
    [CreatedOn]        DATETIME       NOT NULL,
    [UpdatedBy]        NVARCHAR (100) NULL,
    [UpdatedOn]        DATETIME       NULL,
    [Enabled]          BIT            NOT NULL,
    [GenderId]         INT            NULL,
    [OriginalFullName] NVARCHAR (170) NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] ASC)
);










GO
CREATE NONCLUSTERED INDEX [IX_PcpNpi_001]
    ON [Enrollment].[PersonPrimaryCarePhysician]([NPI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PcpFullName_001]
    ON [Enrollment].[PersonPrimaryCarePhysician]([FullName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PcpFederalTaxId_001]
    ON [Enrollment].[PersonPrimaryCarePhysician]([FederalTaxId] ASC);


GO



GO


