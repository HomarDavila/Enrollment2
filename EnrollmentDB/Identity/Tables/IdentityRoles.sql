CREATE TABLE [Identity].[IdentityRoles] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (500) NULL,
    [CreatedBy] VARCHAR (100) COLLATE Modern_Spanish_CI_AS DEFAULT ('System') NULL,
    [CreatedOn] DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedBy] VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [UpdatedOn] DATETIME      NULL,
    [Enabled]   BIT           DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Identity.IdentityRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);





