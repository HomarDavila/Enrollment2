CREATE TABLE [Identity].[IdentityOptionTypes] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (500) COLLATE Modern_Spanish_CI_AS NULL,
    [Code] VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    CONSTRAINT [PK_Identity.IdentityOptionTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);



