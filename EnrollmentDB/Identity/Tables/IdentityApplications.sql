CREATE TABLE [Identity].[IdentityApplications] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (500) COLLATE Modern_Spanish_CI_AS NULL,
    [URL]       VARCHAR (500) COLLATE Modern_Spanish_CI_AS NULL,
    [Code]      VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [CreatedBy] VARCHAR (100) COLLATE Modern_Spanish_CI_AS DEFAULT ('System') NULL,
    [CreatedOn] DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedBy] VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [UpdatedOn] DATETIME      NULL,
    [Enabled]   BIT           DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Identity.IdentityApplications] PRIMARY KEY CLUSTERED ([Id] ASC)
);



