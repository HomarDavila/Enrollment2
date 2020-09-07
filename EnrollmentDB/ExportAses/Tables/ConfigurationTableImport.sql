CREATE TABLE [ExportAses].[ConfigurationTableImport] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [TableName]       VARCHAR (30)  COLLATE Modern_Spanish_CI_AS NOT NULL,
    [ColumnName]      VARCHAR (40)  COLLATE Modern_Spanish_CI_AS NOT NULL,
    [OrdinalPosition] INT           NOT NULL,
    [DataType]        VARCHAR (10)  COLLATE Modern_Spanish_CI_AS NOT NULL,
    [Length]          INT           NULL,
    [CreatedBy]       VARCHAR (50)  COLLATE Modern_Spanish_CI_AS NOT NULL,
    [CreatedOn]       DATETIME      NOT NULL,
    [UpdatedBy]       VARCHAR (50)  COLLATE Modern_Spanish_CI_AS NULL,
    [UpdatedOn]       DATETIME      NULL,
    [Enabled]         BIT           NULL,
    [ProcessType]     NVARCHAR (50) COLLATE Modern_Spanish_CI_AS NOT NULL
);





