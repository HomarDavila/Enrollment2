﻿CREATE TABLE [ExportAses].[HouseHoldRecords] (
    [TranId]          NCHAR (2)     NULL,
    [ProcessDate]     NVARCHAR (10) NULL,
    [MemberId]        NVARCHAR (11) NULL,
    [MPI1]            NVARCHAR (11) NULL,
    [MPI2]            NVARCHAR (11) NULL,
    [MPI3]            NVARCHAR (11) NULL,
    [MPI4]            NVARCHAR (11) NULL,
    [MPI5]            NVARCHAR (11) NULL,
    [MPI6]            NVARCHAR (11) NULL,
    [MPI7]            NVARCHAR (11) NULL,
    [MPI8]            NVARCHAR (11) NULL,
    [MPI9]            NVARCHAR (11) NULL,
    [MPI10]           NVARCHAR (11) NULL,
    [MPI11]           NVARCHAR (11) NULL,
    [MPI12]           NVARCHAR (11) NULL,
    [MPI13]           NVARCHAR (11) NULL,
    [MPI14]           NVARCHAR (11) NULL,
    [MPI15]           NVARCHAR (11) NULL,
    [MPI16]           NVARCHAR (11) NULL,
    [MPI17]           NVARCHAR (11) NULL,
    [MPI18]           NVARCHAR (11) NULL,
    [ProcessHeaderId] INT           NULL,
    CONSTRAINT [FK_HouseHoldRecords_ProcessHeader] FOREIGN KEY ([ProcessHeaderId]) REFERENCES [ExportAses].[ProcessHeader] ([ID])
);
