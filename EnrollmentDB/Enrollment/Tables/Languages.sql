CREATE TABLE [Enrollment].[Languages] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Code]      NCHAR (5)      COLLATE Modern_Spanish_CI_AS NOT NULL,
    [Name]      NVARCHAR (50)  COLLATE Modern_Spanish_CI_AS NOT NULL,
    [CreatedBy] NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [CreatedOn] DATETIME       NOT NULL,
    [UpdatedBy] NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [UpdatedOn] DATETIME       NULL,
    [Enabled]   BIT            NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([Id] ASC)
);



