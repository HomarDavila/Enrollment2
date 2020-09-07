CREATE TABLE [ExportAses].[HouseHoldRecords] (
    [TranId]           NCHAR (2)     COLLATE Modern_Spanish_CI_AS NULL,
    [ProcessDate]      NVARCHAR (10) COLLATE Modern_Spanish_CI_AS NULL,
    [MemberId]         NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI1]             NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI2]             NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI3]             NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI4]             NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI5]             NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI6]             NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI7]             NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI8]             NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI9]             NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI10]            NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI11]            NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI12]            NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI13]            NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI14]            NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI15]            NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI16]            NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI17]            NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [MPI18]            NVARCHAR (11) COLLATE Modern_Spanish_CI_AS NULL,
    [BlockNumber]      NCHAR (6)     COLLATE Modern_Spanish_CI_AS NULL,
    [ProcessHeaderId]  INT           NULL,
    [IsValidForImport] BIT           NULL,
    [MessageInvalid]   VARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    CONSTRAINT [FK_HouseHoldRecords_ProcessHeader] FOREIGN KEY ([ProcessHeaderId]) REFERENCES [ExportAses].[ProcessHeader] ([ID])
);







