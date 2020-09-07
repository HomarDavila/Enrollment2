CREATE TABLE [Enrollment].[Puntuacion] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Puntos]              INT           NULL,
    [PreguntasId]         INT           NULL,
    [EnrollmentHistoryID] INT           NULL,
    [CreatedBy]           VARCHAR (200) NULL,
    [CreatedOn]           DATETIME      NULL,
    [UpdatedBy]           VARCHAR (200) NULL,
    [UpdatedOn]           DATETIME      NULL,
    [Enabled]             BIT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([PreguntasId]) REFERENCES [Enrollment].[Preguntas] ([Id]),
    CONSTRAINT [FK__Puntuacio__Enrol__5F141958] FOREIGN KEY ([EnrollmentHistoryID]) REFERENCES [Enrollment].[EnrollmentHistories] ([Id])
);

