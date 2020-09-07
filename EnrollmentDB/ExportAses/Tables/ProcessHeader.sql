CREATE TABLE [ExportAses].[ProcessHeader] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [FileName]    NVARCHAR (256) NULL,
    [ProcessType] NVARCHAR (100) NOT NULL,
    [StartTime]   DATETIME       NULL,
    [EndTime]     DATETIME       NULL,
    [CreatedBy]   VARCHAR (50)   NOT NULL,
    [CreatedOn]   DATETIME       CONSTRAINT [DF_File_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]   VARCHAR (50)   NULL,
    [UpdatedOn]   DATETIME       NULL,
    [Enabled]     BIT            CONSTRAINT [DF_File_Void] DEFAULT ((0)) NOT NULL,
    [Exception]   VARCHAR (2048) NULL,
    CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED ([ID] ASC)
);





