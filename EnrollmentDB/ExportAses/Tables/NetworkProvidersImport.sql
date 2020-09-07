CREATE TABLE [ExportAses].[NetworkProvidersImport] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [CarrierCode]      NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [ProviderType]     NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [ReportDate]       NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [PMG]              NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [PMGName]          NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [PMGFederalTaxId]  NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [NPI]              NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [FederalTaxId]     NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [SpecialityCode]   NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [AssignedLives]    NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Name]             NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [LastName1]        NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [LastName2]        NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [FirstName]        NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [MiddleName]       NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [AddressLine1]     NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [AddressLine2]     NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [City]             NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [ZipCode]          NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Phone]            NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Fax]              NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Sunday]           NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Monday]           NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Tuesday]          NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Wednesday]        NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Thursday]         NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Friday]           NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Saturday]         NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [LicenseNumber]    NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [ContactPerson]    NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Gender]           NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [Language]         NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [ProviderCapacity] NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [AcceptGender]     NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [AgeAcceptBegin]   NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [AgeAcceptEnd]     NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [ContractStatus]   NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [MunicipalityCode] NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [NPIPmg]           NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [ProcessHeaderId]  INT            NULL,
    [IsValidForImport] BIT            NULL,
    [MessageInvalid]   NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    CONSTRAINT [PK_NetworkProvidersImport] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NetworkProvidersImport_ProcessHeader] FOREIGN KEY ([ProcessHeaderId]) REFERENCES [ExportAses].[ProcessHeader] ([ID])
);








GO
CREATE NONCLUSTERED INDEX [IX_ProcessHeader_v1]
    ON [ExportAses].[NetworkProvidersImport]([ProcessHeaderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_IsValidForImport_v1]
    ON [ExportAses].[NetworkProvidersImport]([IsValidForImport] ASC);

