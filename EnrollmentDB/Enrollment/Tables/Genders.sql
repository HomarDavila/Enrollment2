CREATE TABLE [Enrollment].[Genders] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Code]      NCHAR (5)      NOT NULL,
    [Name]      NVARCHAR (50)  NOT NULL,
    [CreatedBy] NVARCHAR (100) NOT NULL,
    [CreatedOn] DATETIME       NOT NULL,
    [UpdatedBy] NVARCHAR (100) NULL,
    [UpdatedOn] DATETIME       NULL,
    [Enabled]   BIT            NOT NULL,
    CONSTRAINT [PK_Genders] PRIMARY KEY CLUSTERED ([Id] ASC)
);







