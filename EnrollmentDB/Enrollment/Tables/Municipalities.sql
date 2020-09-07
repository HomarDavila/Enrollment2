CREATE TABLE [Enrollment].[Municipalities] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Code]      NCHAR (5)      NULL,
    [Name]      NVARCHAR (50)  NULL,
    [CreatedBy] NVARCHAR (100) NULL,
    [CreatedOn] DATETIME       NOT NULL,
    [UpdatedBy] NVARCHAR (100) NULL,
    [UpdatedOn] DATETIME       NULL,
    [Enabled]   BIT            NOT NULL,
    CONSTRAINT [PK_Municipalities] PRIMARY KEY CLUSTERED ([Id] ASC)
);



