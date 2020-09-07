CREATE TABLE [dbo].[Event] (
    [EventId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [InsertedDate]    DATETIME       DEFAULT (getutcdate()) NOT NULL,
    [LastUpdatedDate] DATETIME       NULL,
    [Data]            NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([EventId] ASC)
);

