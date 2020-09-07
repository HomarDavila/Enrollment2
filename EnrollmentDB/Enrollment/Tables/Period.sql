CREATE TABLE [Enrollment].[Period] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [PeriodIni] DATETIME       NULL,
    [PeriodFin] DATETIME       NULL,
    [CreatedOn] DATETIME       NULL,
    [CreatedBy] NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [UpdatedBy] NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [UpdatedOn] DATETIME       NULL,
    [Enabled]   BIT            NULL,
    CONSTRAINT [PK_Enrollment.Period] PRIMARY KEY CLUSTERED ([Id] ASC)
);





