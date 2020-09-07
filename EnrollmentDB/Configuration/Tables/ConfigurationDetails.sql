CREATE TABLE [Configuration].[ConfigurationDetails] (
    [Id]                          INT           IDENTITY (1, 1) NOT NULL,
    [Code]                        VARCHAR (100) NULL,
    [Description]                 VARCHAR (500) NULL,
    [StringValue]                 VARCHAR (500) NULL,
    [AdditionalStringValue]       VARCHAR (500) NULL,
    [NumericValue]                FLOAT (53)    NULL,
    [AdditionalNumericValue]      FLOAT (53)    NULL,
    [ConfigurationId]             INT           NOT NULL,
    [ParentConfigurationDetailId] INT           NULL,
    [CreatedBy]                   VARCHAR (100) DEFAULT ('System') NULL,
    [CreatedOn]                   DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedBy]                   VARCHAR (100) NULL,
    [UpdatedOn]                   DATETIME      NULL,
    [Enabled]                     BIT           DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Configuration.ConfigurationDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Configuration.ConfigurationDetails_Configuration.Configurations_ConfigurationId] FOREIGN KEY ([ConfigurationId]) REFERENCES [Configuration].[Configurations] ([Id]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [IX_ConfigurationId]
    ON [Configuration].[ConfigurationDetails]([ConfigurationId] ASC);

