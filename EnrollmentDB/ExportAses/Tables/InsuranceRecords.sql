CREATE TABLE [ExportAses].[InsuranceRecords] (
    [TranId]               NCHAR (2)     NULL,
    [ProcessDate]          NVARCHAR (10) NULL,
    [FamilyId]             NVARCHAR (11) NULL,
    [MemberSuffix]         NVARCHAR (2)  NULL,
    [HealthInsurerCode]    NVARCHAR (3)  NULL,
    [PolicyNumber]         NVARCHAR (20) NULL,
    [PolicyExpirationDate] NVARCHAR (10) NULL,
    [CoveredServices]      NVARCHAR (40) NULL,
    [ProcessHeaderId]      INT           NULL,
    CONSTRAINT [FK_InsuranceRecords_ProcessHeader] FOREIGN KEY ([ProcessHeaderId]) REFERENCES [ExportAses].[ProcessHeader] ([ID])
);

