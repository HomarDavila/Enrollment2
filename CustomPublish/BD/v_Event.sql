CREATE VIEW dbo.[v_Event] WITH SCHEMABINDING
AS
SELECT EventId, 
	InsertedDate,
	CAST(JSON_VALUE(Data, '$.EventType') AS NVARCHAR(255)) AS [EventType],
	CAST(JSON_VALUE(Data, '$.ReferenceId') AS NVARCHAR(255)) AS [ReferenceId],
	CAST(JSON_VALUE(Data, '$.Environment.UserName') AS NVARCHAR(50)) AS [UserName],
	JSON_VALUE(Data, '$.Target.Type') As [TargetType],
	COALESCE(JSON_VALUE(Data, '$.Target.Old'), JSON_QUERY(Data, '$.Target.Old')) AS [TargetOld],
	COALESCE(JSON_VALUE(Data, '$.Target.New'), JSON_QUERY(Data, '$.Target.New')) AS [TargetNew],
	JSON_QUERY(Data, '$.Comments') AS [Comments],
	[Data] As [Data]
FROM dbo.[Event]
GO
CREATE NONCLUSTERED INDEX [IX_V_EVENT_EventType_ReferenceId]
    ON [dbo].[v_Event]([EventType] ASC, [ReferenceId] ASC);


GO
CREATE UNIQUE CLUSTERED INDEX [PK_V_EVENT]
    ON [dbo].[v_Event]([EventId] ASC);

