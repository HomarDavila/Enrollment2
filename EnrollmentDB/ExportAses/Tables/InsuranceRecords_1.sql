CREATE TABLE [ExportAses].[InsuranceRecords] (
    [TranId]               NCHAR (2)      COLLATE Modern_Spanish_CI_AS NULL,
    [ProcessDate]          NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [FamilyId]             NVARCHAR (11)  COLLATE Modern_Spanish_CI_AS NULL,
    [MemberSuffix]         NVARCHAR (2)   COLLATE Modern_Spanish_CI_AS NULL,
    [HealthInsurerCode]    NVARCHAR (3)   COLLATE Modern_Spanish_CI_AS NULL,
    [PolicyNumber]         NVARCHAR (20)  COLLATE Modern_Spanish_CI_AS NULL,
    [PolicyExpirationDate] NVARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [CoveredServices]      NVARCHAR (40)  COLLATE Modern_Spanish_CI_AS NULL,
    [BlockNumber]          NCHAR (6)      COLLATE Modern_Spanish_CI_AS NULL,
    [ProcessHeaderId]      INT            NULL,
    [IsValidForImport]     BIT            NULL,
    [MessageInvalid]       NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    CONSTRAINT [FK_InsuranceRecords_ProcessHeader] FOREIGN KEY ([ProcessHeaderId]) REFERENCES [ExportAses].[ProcessHeader] ([ID])
);







