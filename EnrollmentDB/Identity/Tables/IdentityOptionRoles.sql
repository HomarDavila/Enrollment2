CREATE TABLE [Identity].[IdentityOptionRoles] (
    [ApplicationId] INT NOT NULL,
    [OptionId]      INT NOT NULL,
    [RolId]         INT NOT NULL,
    CONSTRAINT [PK_Identity.IdentityOptionRoles] PRIMARY KEY CLUSTERED ([ApplicationId] ASC, [OptionId] ASC, [RolId] ASC),
    CONSTRAINT [FK_Identity.IdentityOptionRoles_Identity.IdentityApplications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [Identity].[IdentityApplications] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Identity.IdentityOptionRoles_Identity.IdentityOptions_OptionId] FOREIGN KEY ([OptionId]) REFERENCES [Identity].[IdentityOptions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Identity.IdentityOptionRoles_Identity.IdentityRoles_RolId] FOREIGN KEY ([RolId]) REFERENCES [Identity].[IdentityRoles] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_RolId]
    ON [Identity].[IdentityOptionRoles]([RolId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OptionId]
    ON [Identity].[IdentityOptionRoles]([OptionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationId]
    ON [Identity].[IdentityOptionRoles]([ApplicationId] ASC);

