CREATE TABLE [Identity].[IdentityUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     INT            NOT NULL,
    [ClaimType]  NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    [ClaimValue] NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    CONSTRAINT [PK_Identity.IdentityUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Identity.IdentityUserClaims_Identity.IdentityUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[IdentityUsers] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [Identity].[IdentityUserClaims]([UserId] ASC);

