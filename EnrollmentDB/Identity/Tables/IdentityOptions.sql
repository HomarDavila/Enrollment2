CREATE TABLE [Identity].[IdentityOptions] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (500) COLLATE Modern_Spanish_CI_AS NULL,
    [Code]         VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [URL]          VARCHAR (500) COLLATE Modern_Spanish_CI_AS NULL,
    [OptionTypeId] INT           NOT NULL,
    [CreatedBy]    VARCHAR (100) COLLATE Modern_Spanish_CI_AS DEFAULT ('System') NULL,
    [CreatedOn]    DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedBy]    VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [UpdatedOn]    DATETIME      NULL,
    [Enabled]      BIT           DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Identity.IdentityOptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Identity.IdentityOptions_Identity.IdentityOptionTypes_OptionTypeId] FOREIGN KEY ([OptionTypeId]) REFERENCES [Identity].[IdentityOptionTypes] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_OptionTypeId]
    ON [Identity].[IdentityOptions]([OptionTypeId] ASC);

