--Reference Data for [Configuration].[Configurations]
SET IDENTITY_INSERT [Configuration].[Configurations] ON 
GO 
MERGE INTO [Configuration].[Configurations] AS Target 
USING (VALUES 
(1, N'QUESTIONSECURITY', N'QUESTIONSECURITY', N'Preguntas de seguridad para determinar si la persona existe en mediti', N'System', GETDATE(), 1)
) 
AS Source (Id, [Name], Code, [Description], CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Name] = Source.[Name] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [Name], Code, [Description], CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [Name], Code, [Description], CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Configuration].[Configurations] OFF 
GO 

--Reference Data for [Configuration].[ConfigurationDetails]
SET IDENTITY_INSERT [Configuration].[ConfigurationDetails] ON 
GO 
MERGE INTO [Configuration].[ConfigurationDetails] AS Target 
USING (VALUES 
(1, N'QSCity', N'City of the last application', N'Ciudad de la última aplicación',1, 1, N'System', GETDATE(), 1),
(2, N'QSName', N'Name of the second person on the lastest application', N'Nombre de la segunda persona en la última aplicación',2, 1,  N'System', GETDATE(), 1),
(3, N'QSMail', N'Email in the latest application', N'Email en la última aplicación', 3, 1, N'System', GETDATE(), 1),
(4, N'QSPhone', N'Phone in the latest application', N'Teléfono en la última aplicación',4, 1, N'System',  GETDATE(), 1)
) 
AS Source (Id, Code, StringValue, AdditionalStringValue, AdditionalNumericValue, ConfigurationId, CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET StringValue = Source.StringValue 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, Code, StringValue, AdditionalStringValue, AdditionalNumericValue, ConfigurationId, CreatedBy, CreatedOn, [Enabled])
VALUES (Id, Code, StringValue, AdditionalStringValue, AdditionalNumericValue, ConfigurationId, CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Configuration].[ConfigurationDetails] OFF 
GO 

--Reference Data for [Identity].[IdentityApplications]
SET IDENTITY_INSERT [Identity].[IdentityApplications] ON 
GO 
MERGE INTO [Identity].[IdentityApplications] AS Target 
USING (VALUES 
(1, N'EnrollmentSelfService', N'/', N'EnrollmentSelfService', N'System', GETDATE(), 1)
) 
AS Source (Id, [Name],[URL], Code, CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Name] = Source.[Name] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [Name],[URL], Code, CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [Name],[URL], Code, CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Identity].[IdentityApplications] OFF 
GO 

--Reference Data for [Identity].[IdentityAudiences]
MERGE INTO [Identity].[IdentityAudiences] AS Target 
USING (VALUES 
(N'b78cef87b8b940afbebde9051904911d', N'k-qHxdXnDfo79y8w0ozX2-8qe1xOdvdacS8_t4QjdIw', N'EnrollmentSelfService')
) 
AS Source (Id, Base64Secret, [Name])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Name] = Source.[Name] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, Base64Secret, [Name])
VALUES (Id, Base64Secret, [Name])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 

--Reference Data for [Identity].[IdentityRoles]
SET IDENTITY_INSERT [Identity].[IdentityRoles] ON 
GO 
MERGE INTO [Identity].[IdentityRoles] AS Target 
USING (VALUES 
(1, N'UserSelfService',N'System', GETDATE(), 1),
(2, N'Administrador',N'System', GETDATE(), 1)
) 
AS Source (Id, [Name], CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Name] = Source.[Name] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [Name], CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [Name], CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Identity].[IdentityRoles] OFF 
GO 



--Reference Data for [Identity].[IdentityOptionTypes]
SET IDENTITY_INSERT [Identity].[IdentityOptionTypes] ON 
GO 
MERGE INTO [Identity].[IdentityOptionTypes] AS Target 
USING (VALUES 
(1, N'Page', N'Page') 
) 
AS Source (Id, [Name],[Code])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Name] = Source.[Name] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [Name],[Code])
VALUES (Id, [Name],[Code])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Identity].[IdentityOptionTypes] OFF 
GO 

--Reference Data for [Identity].[IdentityOptions]
SET IDENTITY_INSERT [Identity].[IdentityOptions] ON 
GO 
MERGE INTO [Identity].[IdentityOptions] AS Target 
USING (VALUES 
(1, N'Home', N'Home', N'/',1,  N'System', GETDATE(), 1) 
) 
AS Source (Id, [Name],[Code], [URL], [OptionTypeId], CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Name] = Source.[Name] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [Name],[Code], [URL], [OptionTypeId], CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [Name],[Code], [URL], [OptionTypeId], CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Identity].[IdentityOptions] OFF 
GO 

