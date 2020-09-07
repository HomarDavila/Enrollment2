CREATE TABLE [Identity].[IdentityRolUsers] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [ApplicationId] INT           NOT NULL,
    [UserId]        INT           NOT NULL,
    [RoleId]        INT           NOT NULL,
    [CreatedBy]     VARCHAR (100) COLLATE Modern_Spanish_CI_AS DEFAULT ('System') NULL,
    [CreatedOn]     DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedBy]     VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [UpdatedOn]     DATETIME      NULL,
    [Enabled]       BIT           DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Identity.IdentityRolUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Identity.IdentityRolUsers_Identity.IdentityRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[IdentityRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Identity.IdentityRolUsers_Identity.IdentityUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[IdentityUsers] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [Identity].[IdentityRolUsers]([RoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [Identity].[IdentityRolUsers]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [I_IdentityRolUsers_UserId]
    ON [Identity].[IdentityRolUsers]([UserId] ASC);

