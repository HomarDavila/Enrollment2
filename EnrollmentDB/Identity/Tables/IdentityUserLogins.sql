CREATE TABLE [Identity].[IdentityUserLogins] (
    [LoginProvider] NVARCHAR (128) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [ProviderKey]   NVARCHAR (128) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [UserId]        INT            NOT NULL,
    CONSTRAINT [PK_Identity.IdentityUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_Identity.IdentityUserLogins_Identity.IdentityUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[IdentityUsers] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [Identity].[IdentityUserLogins]([UserId] ASC);

