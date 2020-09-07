CREATE TABLE [Enrollment].[PcpPmgMco] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [PrimaryCarePhysicianId] INT            NULL,
    [PmgId]                  INT            NULL,
    [McoId]                  INT            NULL,
    [CreatedBy]              NVARCHAR (100) NULL,
    [CreatedOn]              DATETIME       NOT NULL,
    [UpdatedBy]              NVARCHAR (100) NULL,
    [UpdatedOn]              DATETIME       NULL,
    [Enabled]                BIT            NOT NULL,
    CONSTRAINT [PK_PcpPmgMco] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PcpPmgMco_ManagedCareOrganizations] FOREIGN KEY ([McoId]) REFERENCES [Enrollment].[ManagedCareOrganizations] ([Id]),
    CONSTRAINT [FK_PcpPmgMco_PrimaryCarePhysician] FOREIGN KEY ([PrimaryCarePhysicianId]) REFERENCES [Enrollment].[PrimaryCarePhysician] ([Id]),
    CONSTRAINT [FK_PcpPmgMco_PrimaryMedicalGroup] FOREIGN KEY ([PmgId]) REFERENCES [Enrollment].[PrimaryMedicalGroup] ([Id])
);





