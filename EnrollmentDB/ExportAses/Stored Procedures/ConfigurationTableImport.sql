CREATE TABLE [ExportAses].[ConfigurationTableImport] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [TableName]       VARCHAR (30)  NOT NULL,
    [ColumnName]      VARCHAR (40)  NOT NULL,
    [OrdinalPosition] INT           NOT NULL,
    [DataType]        VARCHAR (10)  NOT NULL,
    [Length]          INT           NULL,
    [CreatedBy]       VARCHAR (50)  NOT NULL,
    [CreatedOn]       DATETIME      NOT NULL,
    [UpdatedBy]       VARCHAR (50)  NULL,
    [UpdatedOn]       DATETIME      NULL,
    [Enabled]         BIT           NULL,
    [ProcessType]     NVARCHAR (50) NOT NULL
);



