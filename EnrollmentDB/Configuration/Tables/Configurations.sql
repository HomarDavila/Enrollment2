CREATE TABLE [Configuration].[Configurations] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (100) NULL,
    [Code]        VARCHAR (100) NULL,
    [Description] VARCHAR (500) NULL,
    [CreatedBy]   VARCHAR (100) DEFAULT ('System') NULL,
    [CreatedOn]   DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (100) NULL,
    [UpdatedOn]   DATETIME      NULL,
    [Enabled]     BIT           DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Configuration.Configurations] PRIMARY KEY CLUSTERED ([Id] ASC)
);





