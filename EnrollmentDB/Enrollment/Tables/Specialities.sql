CREATE TABLE [Enrollment].[Specialities] (
    [Id]                              INT            IDENTITY (1, 1) NOT NULL,
    [Code]                            NCHAR (5)      NULL,
    [Name]                            NVARCHAR (200) NULL,
    [ShowItOnChangeEnrollmentProcess] BIT            NULL,
    [CreatedBy]                       NVARCHAR (100) NULL,
    [CreatedOn]                       DATETIME       NOT NULL,
    [UpdatedBy]                       NVARCHAR (100) NULL,
    [UpdatedOn]                       DATETIME       NULL,
    [Enabled]                         BIT            NOT NULL,
    [Nombre]                          NVARCHAR (200) NULL,
    CONSTRAINT [PK_Specialities] PRIMARY KEY CLUSTERED ([Id] ASC)
);







