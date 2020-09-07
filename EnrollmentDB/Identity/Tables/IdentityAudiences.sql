CREATE TABLE [Identity].[IdentityAudiences] (
    [Id]           VARCHAR (32)  COLLATE Modern_Spanish_CI_AS NOT NULL,
    [Base64Secret] VARCHAR (80)  COLLATE Modern_Spanish_CI_AS NULL,
    [Name]         VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    CONSTRAINT [PK_Identity.IdentityAudiences] PRIMARY KEY CLUSTERED ([Id] ASC)
);



