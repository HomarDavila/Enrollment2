CREATE TABLE [dbo].[Event] (
    [EventId]         INT            IDENTITY (1, 1) NOT NULL,
    [InsertedDate]    DATETIME       NULL,
    [LastUpdatedDate] DATETIME       NULL,
    [Data]            NVARCHAR (MAX) COLLATE Modern_Spanish_CI_AS NULL,
    CONSTRAINT [PK_dbo.Event] PRIMARY KEY CLUSTERED ([EventId] ASC)
);




GO
CREATE NONCLUSTERED INDEX [I_Event_Data]
    ON [dbo].[Event]([InsertedDate] ASC);

