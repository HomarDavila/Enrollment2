CREATE TABLE [Enrollment].[PrimaryCarePhysicianDetail] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [Phone]                 NVARCHAR (20)  NULL,
    [CreatedBy]             NVARCHAR (100) NULL,
    [CreatedOn]             DATETIME       NULL,
    [UpdatedBy]             NVARCHAR (100) NULL,
    [UpdatedOn]             DATETIME       NULL,
    [Enabled]               BIT            NULL,
    [AddressLineOne]        NVARCHAR (45)  NULL,
    [AddressLineTwo]        NVARCHAR (45)  NULL,
    [City]                  NVARCHAR (45)  NULL,
    [ZipCode]               NVARCHAR (9)   NULL,
    [State]                 NVARCHAR (9)   NULL,
    [PcpPmgMcoId]           INT            NULL,
    [MunicipalityId]        INT            NULL,
    [AmountOfLivesEnrolled] INT            NULL,
    [Capacity]              INT            NULL,
    CONSTRAINT [PK_PrimaryCarePhysicianDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([MunicipalityId]) REFERENCES [Enrollment].[Municipalities] ([Id]),
    FOREIGN KEY ([PcpPmgMcoId]) REFERENCES [Enrollment].[PcpPmgMco] ([Id])
);







