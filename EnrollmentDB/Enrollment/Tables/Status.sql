CREATE TABLE [Enrollment].[Status] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [NameES]         NVARCHAR (50) NOT NULL,
    [NameEN]         NVARCHAR (50) NOT NULL,
    [Description]    NVARCHAR (50) NULL,
    [CreatedBy]      NVARCHAR (50) NOT NULL,
    [CreatedOn]      DATETIME      NOT NULL,
    [UpdatedBy]      NVARCHAR (50) NULL,
    [UpdatedOn]      DATETIME      NULL,
    [Enabled]        BIT           NULL,
    [AllowChange]    BIT           NULL,
    [BusinessStatus] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([Id] ASC)
);

