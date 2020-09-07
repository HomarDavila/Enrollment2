CREATE TABLE [Identity].[IdentityUsers] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [UserName]              VARCHAR (500) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [FirstName]             VARCHAR (30)  COLLATE Modern_Spanish_CI_AS NULL,
    [LastName1]             VARCHAR (30)  COLLATE Modern_Spanish_CI_AS NULL,
    [LastName2]             VARCHAR (30)  COLLATE Modern_Spanish_CI_AS NULL,
    [SSNLast4]              VARCHAR (9)   COLLATE Modern_Spanish_CI_AS NULL,
    [ZipCode]               VARCHAR (50)  COLLATE Modern_Spanish_CI_AS NULL,
    [DateOfBirth]           DATETIME      NULL,
    [IsAdministrator]       BIT           NULL,
    [Email]                 VARCHAR (500) COLLATE Modern_Spanish_CI_AS NULL,
    [EmailConfirmed]        BIT           NOT NULL,
    [PasswordHash]          VARCHAR (500) COLLATE Modern_Spanish_CI_AS NULL,
    [SecurityStamp]         VARCHAR (500) COLLATE Modern_Spanish_CI_AS NULL,
    [PhoneNumber]           VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [PhoneNumberConfirmed]  BIT           NOT NULL,
    [TwoFactorEnabled]      BIT           NOT NULL,
    [LockoutEndDateUtc]     DATETIME      NULL,
    [LockoutEnabled]        BIT           NOT NULL,
    [AccessFailedCount]     INT           NOT NULL,
    [CreatedBy]             VARCHAR (100) COLLATE Modern_Spanish_CI_AS CONSTRAINT [DF__IdentityU__Creat__7E37BEF6] DEFAULT ('System') NULL,
    [CreatedOn]             DATETIME      CONSTRAINT [DF__IdentityU__Creat__7F2BE32F] DEFAULT (getdate()) NULL,
    [UpdatedBy]             VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [UpdatedOn]             DATETIME      NULL,
    [Enabled]               BIT           CONSTRAINT [DF__IdentityU__Enabl__00200768] DEFAULT ((1)) NULL,
    [MemberId]              INT           NULL,
    [Roles]                 VARCHAR (10)  COLLATE Modern_Spanish_CI_AS NULL,
    [OptIn]                 BIT           CONSTRAINT [DF_IdentityUsers_OptIn] DEFAULT ((1)) NULL,
    [PhoneNumber2]          VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [Email2]                VARCHAR (500) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI]                   VARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [HasDefaultCredentials] BIT           NOT NULL,
    CONSTRAINT [PK_Identity.IdentityUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


















GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserName]
    ON [Identity].[IdentityUsers]([UserName] ASC);


GO
ALTER INDEX [IX_UserName]
    ON [Identity].[IdentityUsers] DISABLE;




GO
CREATE NONCLUSTERED INDEX [I_IdentityUsers_UserName]
    ON [Identity].[IdentityUsers]([UserName] ASC);


GO
ALTER INDEX [I_IdentityUsers_UserName]
    ON [Identity].[IdentityUsers] DISABLE;




GO
CREATE NONCLUSTERED INDEX [I_IdentityUsers_Password]
    ON [Identity].[IdentityUsers]([PasswordHash] ASC);

