CREATE TABLE [Enrollment].[Preguntas] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Spanish]      VARCHAR (MAX) NULL,
    [English]      VARCHAR (MAX) NULL,
    [CreatedBy]    VARCHAR (200) NULL,
    [CreatedOn]    DATETIME      NULL,
    [UpdatedBy]    VARCHAR (200) NULL,
    [UpdatedOn]    DATETIME      NULL,
    [Enabled]      BIT           NULL,
    [TipoPregunta] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

