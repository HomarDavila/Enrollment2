--Reference Data for [Enrollment].[Period]
SET IDENTITY_INSERT [Enrollment].[Period] ON 
GO 
MERGE INTO [Enrollment].[Period] AS Target 
USING (VALUES 
(1, N'2019-11-01 00:00:00.000', N'2020-01-31 00:00:00.000', N'2019-07-09 00:00:00.000')
) 
AS Source (Id,[PeriodIni],[PeriodFin],[CreatedOn])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id,[PeriodIni],[PeriodFin],[CreatedOn])
VALUES (Id,[PeriodIni],[PeriodFin],[CreatedOn])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Enrollment].[Period] OFF 
GO 


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
(1, N'EnrollmentSelfService', N'/', N'EnrollmentSelfService', N'System', GETDATE(), 1),
(2, N'EnrollmentSystem', N'/', N'EnrollmentSystem', N'System', GETDATE(), 1)
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
(N'b78cef87b8b940afbebde9051904911d', N'k-qHxdXnDfo79y8w0ozX2-8qe1xOdvdacS8_t4QjdIw', N'EnrollmentSelfService'),
(N'173fa47b289d4796a67adf88bbf1d8a5', N'swRp3uqAKAqivuU21ziK30f0cjAEGKAVTo4exKEdxaM', N'EnrollmentSystem')
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
(1, N'AdministradorSelfService',N'System', GETDATE(), 1),
(2, N'AdministradorSystem',N'System', GETDATE(), 1),
(3, N'UserEnrollmentSelfService',N'System', GETDATE(), 1),
(4, N'UserEnrollmentSystem',N'System', GETDATE(), 1)
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
(1, N'Home', N'Home', N'/',1,  N'System', GETDATE(), 1), 
(2, N'Home', N'HomeSelfServices', N'/',1,  N'System', GETDATE(), 1)
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

--Reference Data for [Identity].[IdentityUsers]
SET IDENTITY_INSERT [Identity].[IdentityUsers] ON 
GO 
MERGE INTO [Identity].[IdentityUsers] AS Target 
USING (VALUES 
(1, N'administrador@enrollmentselfservices.com', N'Administrador', N'Administrador', 'administrador@enrollmentselfservices.com',0, 'AOKy5vitWPD4FZMjq0fkWaXY3dU1A0+7w2JUVpSWoPKxh5oPjNePJyb3cTfCceBuwg==', '349c5e50-092a-470c-8050-8c43cc81075d', 0,0,0,0,  N'System', GETDATE(), 1) ,
(2, N'administrador@enrollmentsystem.com', N'Administrador', N'Administrador', 'administrador@enrollmentsystem.com',0, 'AKwmn0IzE0+WCixHsl07MvdZs4JrkdcmDE3fnjDRjCTE+8mOFSv3tn3Wrs4g47BIjQ==', '5b92cc69-dc3c-4fc0-b0e9-65b6f5cf31c5', 0,0,0,0,  N'System', GETDATE(), 1) 
) 
AS Source (Id, [UserName],[FirstName], [LastName1], [Email],[EmailConfirmed], [PasswordHash],[SecurityStamp],[PhoneNumberConfirmed], [TwoFactorEnabled],[LockoutEnabled],[AccessFailedCount] , CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Email] = Source.[Email] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [UserName],[FirstName], [LastName1], [Email],[EmailConfirmed], [PasswordHash],[SecurityStamp],[PhoneNumberConfirmed], [TwoFactorEnabled],[LockoutEnabled],[AccessFailedCount] , CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [UserName],[FirstName], [LastName1], [Email],[EmailConfirmed], [PasswordHash],[SecurityStamp],[PhoneNumberConfirmed], [TwoFactorEnabled],[LockoutEnabled],[AccessFailedCount] , CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Identity].[IdentityUsers] OFF 
GO 

--Reference Data for [Identity].[IdentityRolUsers]
SET IDENTITY_INSERT [Identity].[IdentityRolUsers] ON 
GO 
MERGE INTO [Identity].[IdentityRolUsers] AS Target 
USING (VALUES 
(1, 1, 1, 1, N'System', GETDATE(), 1) ,
(2, 2, 2, 2, N'System', GETDATE(), 1) 
) 
AS Source (Id, [ApplicationId],[UserId], [RoleId], CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [ApplicationId],[UserId], [RoleId], CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [ApplicationId],[UserId], [RoleId], CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Identity].[IdentityRolUsers] OFF 
GO 



--Reference Data for [Identity].[IdentityOptionRoles]
MERGE INTO [Identity].[IdentityOptionRoles] AS Target 
USING (VALUES 
(1, 1, 2) ,
(1, 2, 2) ,
(2, 3, 1) ,
(2, 4, 1)
) 
AS Source ([ApplicationId], [RolId], [OptionId])
ON Target.[ApplicationId] = Source.[ApplicationId] and 
   Target.[OptionId] = Source.[OptionId]  and
   Target.[RolId] = Source.[RolId] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([ApplicationId], [RolId], [OptionId])
VALUES ([ApplicationId], [RolId], [OptionId])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 


--Reference Data for [Enrollment].[[ContractsStatus]
SET IDENTITY_INSERT [Enrollment].[ContractsStatus] ON 
GO 
MERGE INTO [Enrollment].[ContractsStatus] AS Target 
USING (VALUES 
(1, N'Approved Contract', N'ACT',  N'System', GETDATE(), 1), 
(2, N'Unapproved Contract', N'UCT',  N'System', GETDATE(), 1),
(3, N'Letter of Intent', N'LOI',  N'System', GETDATE(), 1)
) 
AS Source (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Name] = Source.[Name],
		   [Code] = Source.[Code]	   
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Enrollment].[ContractsStatus] OFF 
GO 

--Reference Data for [Enrollment].[Genders]
SET IDENTITY_INSERT [Enrollment].[Genders] ON 
GO 
MERGE INTO [Enrollment].[Genders] AS Target 
USING (VALUES 
(1, N'Male',  N'M', N'System', GETDATE(), 1), 
(2, N'Female', N'F',  N'System', GETDATE(), 1),
(3, N'All', N'A',  N'System', GETDATE(), 1)
) 
AS Source (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Name] = Source.[Name],
		   [Code] = Source.[Code]	   
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Enrollment].[Genders] OFF 
GO 

--Reference Data for [Enrollment].[Languages]
SET IDENTITY_INSERT [Enrollment].[Languages] ON 
GO 
MERGE INTO [Enrollment].[Languages] AS Target 
USING (VALUES 
(1, N'English',  N'EN', N'System', GETDATE(), 1), 
(2, N'Spanish', N'ES',  N'System', GETDATE(), 1)
) 
AS Source (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Name] = Source.[Name],
		   [Code] = Source.[Code]	   
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Enrollment].[Languages] OFF 
GO 


--Reference Data for [Enrollment].[Municipalities]
SET IDENTITY_INSERT [Enrollment].[Municipalities] ON 
GO 
MERGE INTO [Enrollment].[Municipalities] AS Target 
USING (VALUES 
(1, N'Adjuntas',  N'0004', N'System', GETDATE(), 1),
(2, N'Aguada',  N'0008', N'System', GETDATE(), 1),
(3, N'Aguadilla',  N'0012', N'System', GETDATE(), 1),
(4, N'Aguas Buenas',  N'0016', N'System', GETDATE(), 1),
(5, N'Aibonito',  N'0020', N'System', GETDATE(), 1),
(6, N'Añasco',  N'0024', N'System', GETDATE(), 1),
(7, N'Arecibo',  N'0028', N'System', GETDATE(), 1),
(8, N'Arroyo',  N'0032', N'System', GETDATE(), 1),
(9, N'Barceloneta',  N'0036', N'System', GETDATE(), 1),
(10, N'Barranquitas',  N'0040', N'System', GETDATE(), 1),
(11, N'Bayamón',  N'0044', N'System', GETDATE(), 1),
(12, N'Cabo Rojo',  N'0048', N'System', GETDATE(), 1),
(13, N'Caguas',  N'0052', N'System', GETDATE(), 1),
(14, N'Camuy',  N'0056', N'System', GETDATE(), 1),
(15, N'Canovanas',  N'0060', N'System', GETDATE(), 1),
(16, N'Carolina',  N'0064', N'System', GETDATE(), 1),
(17, N'Cataño',  N'0068', N'System', GETDATE(), 1),
(18, N'Cayey',  N'0072', N'System', GETDATE(), 1),
(19, N'Ceiba',  N'0076', N'System', GETDATE(), 1),
(20, N'Ciales',  N'0080', N'System', GETDATE(), 1),
(21, N'Cidra',  N'0084', N'System', GETDATE(), 1),
(22, N'Coamo',  N'0088', N'System', GETDATE(), 1),
(23, N'Comerio',  N'0092', N'System', GETDATE(), 1),
(24, N'Corozal',  N'0096', N'System', GETDATE(), 1),
(25, N'Culebra',  N'0100', N'System', GETDATE(), 1),
(26, N'Dorado',  N'0104', N'System', GETDATE(), 1),
(27, N'Fajardo',  N'0108', N'System', GETDATE(), 1),
(28, N'Florida',  N'0112', N'System', GETDATE(), 1),
(29, N'Guanica',  N'0116', N'System', GETDATE(), 1),
(30, N'Guayama',  N'0120', N'System', GETDATE(), 1),
(31, N'Guayanilla',  N'0124', N'System', GETDATE(), 1),
(32, N'Guaynabo',  N'0128', N'System', GETDATE(), 1),
(33, N'Gurabo',  N'0132', N'System', GETDATE(), 1),
(34, N'Hatillo',  N'0136', N'System', GETDATE(), 1),
(35, N'Hormigueros',  N'0140', N'System', GETDATE(), 1),
(36, N'Humacao',  N'0144', N'System', GETDATE(), 1),
(37, N'Isabela',  N'0148', N'System', GETDATE(), 1),
(38, N'Jayuya',  N'0152', N'System', GETDATE(), 1),
(39, N'Juana Diaz',  N'0156', N'System', GETDATE(), 1),
(40, N'Juncos',  N'0160', N'System', GETDATE(), 1),
(41, N'Lajas',  N'0164', N'System', GETDATE(), 1),
(42, N'Lares',  N'0168', N'System', GETDATE(), 1),
(43, N'Las Marias',  N'0172', N'System', GETDATE(), 1),
(44, N'Las Piedras',  N'0176', N'System', GETDATE(), 1),
(45, N'Loiza',  N'0180', N'System', GETDATE(), 1),
(46, N'Luquillo',  N'0184', N'System', GETDATE(), 1),
(47, N'Manatí',  N'0188', N'System', GETDATE(), 1),
(48, N'Maricao',  N'0192', N'System', GETDATE(), 1),
(49, N'Maunabo',  N'0196', N'System', GETDATE(), 1),
(50, N'Mayagüez',  N'0200', N'System', GETDATE(), 1),
(51, N'Moca',  N'0204', N'System', GETDATE(), 1),
(52, N'Morovis',  N'0208', N'System', GETDATE(), 1),
(53, N'Naguabo',  N'0212', N'System', GETDATE(), 1),
(54, N'Naranjito',  N'0216', N'System', GETDATE(), 1),
(55, N'Orocovis',  N'0220', N'System', GETDATE(), 1),
(56, N'Patillas',  N'0224', N'System', GETDATE(), 1),
(57, N'Peñuelas',  N'0228', N'System', GETDATE(), 1),
(58, N'Ponce',  N'0232', N'System', GETDATE(), 1),
(59, N'Puerta de Tierra',  N'0264', N'System', GETDATE(), 1),
(60, N'Puerto Nuevo',  N'0270', N'System', GETDATE(), 1),
(61, N'Quebradillas',  N'0236', N'System', GETDATE(), 1),
(62, N'Rincon',  N'0240', N'System', GETDATE(), 1),
(63, N'Rio Grande',  N'0244', N'System', GETDATE(), 1),
(64, N'Rio Piedras',  N'0272', N'System', GETDATE(), 1),
(65, N'Sabana Grande',  N'0248', N'System', GETDATE(), 1),
(66, N'Salinas',  N'0252', N'System', GETDATE(), 1),
(67, N'San German',  N'0256', N'System', GETDATE(), 1),
(68, N'San José',  N'0274', N'System', GETDATE(), 1),
(69, N'San Juan',  N'0266', N'System', GETDATE(), 1),
(70, N'San Lorenzo',  N'0276', N'System', GETDATE(), 1),
(71, N'San Sebastian',  N'0280', N'System', GETDATE(), 1),
(72, N'Santa Isabel',  N'0284', N'System', GETDATE(), 1),
(73, N'Toa Alta',  N'0288', N'System', GETDATE(), 1),
(74, N'Toa Baja',  N'0292', N'System', GETDATE(), 1),
(75, N'Trujillo Alto',  N'0296', N'System', GETDATE(), 1),
(76, N'Utuado',  N'0300', N'System', GETDATE(), 1),
(77, N'Vega Alta',  N'0304', N'System', GETDATE(), 1),
(78, N'Vega Baja',  N'0308', N'System', GETDATE(), 1),
(79, N'Vieques',  N'0312', N'System', GETDATE(), 1),
(80, N'Villalba',  N'0316', N'System', GETDATE(), 1),
(81, N'Yabucoa',  N'0320', N'System', GETDATE(), 1),
(82, N'Yauco',  N'0324', N'System', GETDATE(), 1),
(83, N'Outside Puerto Rico',  N'0666', N'System', GETDATE(), 1)
) 
AS Source (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Name] = Source.[Name],
		   [Code] = Source.[Code]	   
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Enrollment].[Municipalities] OFF 
GO 

--Reference Data for [Enrollment].[Specialities]
SET IDENTITY_INSERT [Enrollment].[Specialities] ON 
GO 
MERGE INTO [Enrollment].[Specialities] AS Target 
USING (VALUES 
(1, N'General Practice',  N'01', N'System', GETDATE(), 1),
(2, N'General Surgery',  N'02', N'System', GETDATE(), 1),
(3, N'Allergy/Immunology',  N'03', N'System', GETDATE(), 1),
(4, N'Otolaryngology',  N'04', N'System', GETDATE(), 1),
(5, N'Anesthesiology',  N'05', N'System', GETDATE(), 1),
(6, N'Cardiology',  N'06', N'System', GETDATE(), 1),
(7, N'Dermatology',  N'07', N'System', GETDATE(), 1),
(8, N'Family Practice',  N'08', N'System', GETDATE(), 1),
(9, N'Interventional Pain Management',  N'09', N'System', GETDATE(), 1),
(10, N'Gastroenterology',  N'10', N'System', GETDATE(), 1),
(11, N'Internal Medicine',  N'11', N'System', GETDATE(), 1),
(12, N'Osteopathic Manipulative Therapy',  N'12', N'System', GETDATE(), 1),
(13, N'Neurology',  N'13', N'System', GETDATE(), 1),
(14, N'Neurosurgery',  N'14', N'System', GETDATE(), 1),
(15, N'Speech Language Pathologist in Private Practice',  N'15', N'System', GETDATE(), 1),
(16, N'Obstetrics / Gynecology',  N'16', N'System', GETDATE(), 1),
(17, N'Hospice and palliative care',  N'17', N'System', GETDATE(), 1),
(18, N'Ophthalmology',  N'18', N'System', GETDATE(), 1),
(19, N'Oral Surgery',  N'19', N'System', GETDATE(), 1),
(20, N'Orthopedic Surgery',  N'20', N'System', GETDATE(), 1),
(21, N'Cardiac electrophysiology',  N'21', N'System', GETDATE(), 1),
(22, N'Pathology',  N'22', N'System', GETDATE(), 1),
(23, N'Sports medicine',  N'23', N'System', GETDATE(), 1),
(24, N'Plastic and Reconstructive Surgery',  N'24', N'System', GETDATE(), 1),
(25, N'Physical Medicine / Rehabilitation',  N'25', N'System', GETDATE(), 1),
(26, N'Psychiatry',  N'26', N'System', GETDATE(), 1),
(27, N'Geriatric psychiatry',  N'27', N'System', GETDATE(), 1),
(28, N'Colorectal Surgery (Formerly Proctology)',  N'28', N'System', GETDATE(), 1),
(29, N'Pulmonary Diseases',  N'29', N'System', GETDATE(), 1),
(30, N'Diagnostic Radiology',  N'30', N'System', GETDATE(), 1),
(31, N'Intensive cardiac rehabilitation',  N'31', N'System', GETDATE(), 1),
(32, N'Anesthesiologist Assistant',  N'32', N'System', GETDATE(), 1),
(33, N'Thoracic Surgery',  N'33', N'System', GETDATE(), 1),
(34, N'Urology',  N'34', N'System', GETDATE(), 1),
(35, N'Chiropractic',  N'35', N'System', GETDATE(), 1),
(36, N'Nuclear Medicine',  N'36', N'System', GETDATE(), 1),
(37, N'Pediatric Medicine',  N'37', N'System', GETDATE(), 1),
(38, N'Geriatric Medicine',  N'38', N'System', GETDATE(), 1),
(39, N'Nephrology',  N'39', N'System', GETDATE(), 1),
(40, N'Hand Surgery',  N'40', N'System', GETDATE(), 1),
(41, N'Optometry',  N'41', N'System', GETDATE(), 1),
(42, N'Certified Nurse Midwife',  N'42', N'System', GETDATE(), 1),
(43, N'Certified Registered Nurse Assistant (CRNA)',  N'43', N'System', GETDATE(), 1),
(44, N'Infectious Disease',  N'44', N'System', GETDATE(), 1),
(45, N'Mammography Screening Center',  N'45', N'System', GETDATE(), 1),
(46, N'Endocrinology',  N'46', N'System', GETDATE(), 1),
(47, N'Independent Diagnostics Testing Facility',  N'47', N'System', GETDATE(), 1),
(48, N'Podiatry',  N'48', N'System', GETDATE(), 1),
(49, N'Ambulatory Surgical Center',  N'49', N'System', GETDATE(), 1),
(50, N'Nurse Practitioner',  N'50', N'System', GETDATE(), 1),
(51, N'Medical Supply Company with Orthotist',  N'51', N'System', GETDATE(), 1),
(52, N'Medical Supply Company with Prosthetist',  N'52', N'System', GETDATE(), 1),
(53, N'Medical Supply Company with Orthotist-Prosthetist',  N'53', N'System', GETDATE(), 1),
(54, N'Other Medical Supply Company',  N'54', N'System', GETDATE(), 1),
(55, N'Individual Certified Orthotist',  N'55', N'System', GETDATE(), 1),
(56, N'Individual Certified Prosthetist',  N'56', N'System', GETDATE(), 1),
(57, N'Individual Certified Orthotist-Prosthetist',  N'57', N'System', GETDATE(), 1),
(58, N'Medical Supply Company with pharmacist',  N'58', N'System', GETDATE(), 1),
(59, N'Ambulance Service Provider',  N'59', N'System', GETDATE(), 1),
(60, N'Public Health and Welfare Agency',  N'60', N'System', GETDATE(), 1),
(61, N'Voluntary Health or Charitable Agency',  N'61', N'System', GETDATE(), 1),
(62, N'Psychologist',  N'62', N'System', GETDATE(), 1),
(63, N'Portable X-ray Supplier',  N'63', N'System', GETDATE(), 1),
(64, N'Audiologist',  N'64', N'System', GETDATE(), 1),
(65, N'Physical Therapist',  N'65', N'System', GETDATE(), 1),
(66, N'Rheumatology',  N'66', N'System', GETDATE(), 1),
(67, N'Occupational Therapy',  N'67', N'System', GETDATE(), 1),
(68, N'Clinical Psychologist',  N'68', N'System', GETDATE(), 1),
(69, N'Clinical Laboratory',  N'69', N'System', GETDATE(), 1),
(70, N'Multi-Specialty Clinic or Group Practice',  N'70', N'System', GETDATE(), 1),
(71, N'Registered Dietician / Nutritional Professional',  N'71', N'System', GETDATE(), 1),
(72, N'Pain Management',  N'72', N'System', GETDATE(), 1),
(73, N'Mass Immunization Roster Billers',  N'73', N'System', GETDATE(), 1),
(74, N'Radiation Therapy Center',  N'74', N'System', GETDATE(), 1),
(75, N'Slide Preparation Facilities',  N'75', N'System', GETDATE(), 1),
(76, N'Peripheral Vascular Disease',  N'76', N'System', GETDATE(), 1),
(77, N'Vascular Surgery',  N'77', N'System', GETDATE(), 1),
(78, N'Cardiac Surgery',  N'78', N'System', GETDATE(), 1),
(79, N'Addiction Medicine',  N'79', N'System', GETDATE(), 1),
(80, N'Licensed Clinical Social Worker',  N'80', N'System', GETDATE(), 1),
(81, N'Critical Care (Intensivists)',  N'81', N'System', GETDATE(), 1),
(82, N'Hematology',  N'82', N'System', GETDATE(), 1),
(83, N'Hematology / Oncology',  N'83', N'System', GETDATE(), 1),
(84, N'Preventive Medicine',  N'84', N'System', GETDATE(), 1),
(85, N'Maxillofacial Surgery',  N'85', N'System', GETDATE(), 1),
(86, N'Neuropsychiatry',  N'86', N'System', GETDATE(), 1),
(87, N'All Other Suppliers',  N'87', N'System', GETDATE(), 1),
(88, N'Unknown Supplier / Provider Specialty',  N'88', N'System', GETDATE(), 1),
(89, N'Certified Clinical Nurse Specialist',  N'89', N'System', GETDATE(), 1),
(90, N'Medical Oncology',  N'90', N'System', GETDATE(), 1),
(91, N'Surgical Oncology',  N'91', N'System', GETDATE(), 1),
(92, N'Radiation Oncology',  N'92', N'System', GETDATE(), 1),
(93, N'Emergency Medicine',  N'93', N'System', GETDATE(), 1),
(94, N'Intervention Radiology',  N'94', N'System', GETDATE(), 1),
(95, N'Optician',  N'96', N'System', GETDATE(), 1),
(96, N'Physician Assistant',  N'97', N'System', GETDATE(), 1),
(97, N'Gynecological Oncology',  N'98', N'System', GETDATE(), 1),
(98, N'Unknown Physician Specialty',  N'99', N'System', GETDATE(), 1),
(99, N'Skilled Nursing Facility',  N'A1', N'System', GETDATE(), 1),
(100, N'Intermediate Care Nursing Facility',  N'A2', N'System', GETDATE(), 1),
(101, N'Other Nursing Facility',  N'A3', N'System', GETDATE(), 1),
(102, N'Home Health Agency',  N'A4', N'System', GETDATE(), 1),
(103, N'Pharmacy',  N'A5', N'System', GETDATE(), 1),
(104, N'Medical Supply Company with Respiratory Therapist',  N'A6', N'System', GETDATE(), 1),
(105, N'Department Store',  N'A7', N'System', GETDATE(), 1),
(106, N'Grocery Store',  N'A8', N'System', GETDATE(), 1),
(107, N'Blood Bank',  N'BB', N'System', GETDATE(), 1),
(108, N'Cardiac Catheterization Facility',  N'CV', N'System', GETDATE(), 1),
(109, N'Detox Center',  N'DC', N'System', GETDATE(), 1),
(110, N'Dentist',  N'DD', N'System', GETDATE(), 1),
(111, N'Dialysis Facility',  N'DF', N'System', GETDATE(), 1),
(112, N'Emergency Care Facility',  N'EC', N'System', GETDATE(), 1),
(113, N'Endodontist',  N'EN', N'System', GETDATE(), 1),
(114, N'Geneticist',  N'G1', N'System', GETDATE(), 1),
(115, N'Health Educator',  N'HE', N'System', GETDATE(), 1),
(116, N'Home Health Nurse',  N'HN', N'System', GETDATE(), 1),
(117, N'HIV Ambulatory Antibiotic Facility',  N'HV', N'System', GETDATE(), 1),
(118, N'Intensive Care Unit',  N'IC', N'System', GETDATE(), 1),
(119, N'Infusion Therapy',  N'IT', N'System', GETDATE(), 1),
(120, N'Lithotripsy',  N'LI', N'System', GETDATE(), 1),
(121, N'Neonatology',  N'N1', N'System', GETDATE(), 1),
(122, N'Neonatal ICU',  N'NI', N'System', GETDATE(), 1),
(123, N'Occupational Medicine',  N'O1', N'System', GETDATE(), 1),
(124, N'Optical',  N'OP', N'System', GETDATE(), 1),
(125, N'Perinatology',  N'P1', N'System', GETDATE(), 1),
(126, N'Pediatric Surgery',  N'P2', N'System', GETDATE(), 1),
(127, N'Clinic – Primary Level',  N'PC', N'System', GETDATE(), 1),
(128, N'Periodontist',  N'PE', N'System', GETDATE(), 1),
(129, N'Private Hospital',  N'PH', N'System', GETDATE(), 1),
(130, N'Private Psychiatric Hospital',  N'PP', N'System', GETDATE(), 1),
(131, N'Psychiatric Partial Hospital',  N'PS', N'System', GETDATE(), 1),
(132, N'Respiratory Therapist',  N'RT', N'System', GETDATE(), 1),
(133, N'State Hospital',  N'SH', N'System', GETDATE(), 1),
(134, N'State Psychiatric Hospital',  N'SP', N'System', GETDATE(), 1),
(135, N'Short Term Intervention Center (Behavioral Health-Stabilization Unit)',  N'ST', N'System', GETDATE(), 1),
(136, N'X-ray Facility',  N'XR', N'System', GETDATE(), 1),
(137, N'Cardiovascular Surgery Program',  N'Z4', N'System', GETDATE(), 1)
) 
AS Source (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [Name] = Source.[Name],
		   [Code] = Source.[Code]	   
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [Name],[Code], CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Enrollment].[Specialities] OFF 
GO 

UPDATE [Enrollment].[Specialities] 
set ShowItOnChangeEnrollmentProcess = 1
where Code in ('01', '08', '11', '37', '38')

GO
--Reference Data for [Enrollment].[ManagedCareOrganizations]
SET IDENTITY_INSERT [Enrollment].[ManagedCareOrganizations] ON 
GO 
MERGE INTO [Enrollment].[ManagedCareOrganizations] AS Target 
USING (VALUES 
(1, N'09',  N'First Medical', 300000,  0, NULL, NULL, N'System', GETDATE(), 1), 
(2, N'10', N'MMM', 350000, 0, NULL, NULL, N'System', GETDATE(), 1),
(3, N'11', N'Molina Health Care', 400000, 0, NULL, NULL, N'System', GETDATE(), 1),
(4, N'12', N'Plan Menonita', 200000, 0, NULL, NULL, N'System', GETDATE(), 1),
(5, N'13', N'Triple S',  400000, 0, NULL, NULL, N'System', GETDATE(), 1)
) 
AS Source (Id, [CarrierCode],[CarrierName],[Capacity], [AmountOfLivesEnrolled], [NPI], [EIN] , CreatedBy, CreatedOn, [Enabled])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [CarrierName] = Source.[CarrierName],
		   [CarrierCode] = Source.[CarrierCode],
		   [Capacity] = Source.[Capacity],	   		   
		   [AmountOfLivesEnrolled] = Source.[AmountOfLivesEnrolled],		   
		   [NPI] = Source.[NPI],	
		   [EIN] = Source.[EIN]	
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [CarrierCode],[CarrierName],[Capacity], [AmountOfLivesEnrolled], [NPI], [EIN] , CreatedBy, CreatedOn, [Enabled])
VALUES (Id, [CarrierCode],[CarrierName],[Capacity], [AmountOfLivesEnrolled], [NPI], [EIN] , CreatedBy, CreatedOn, [Enabled])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [Enrollment].[ManagedCareOrganizations] OFF 
GO 

--Reference Data for [ExportAses].[ConfigurationTableImport]
SET IDENTITY_INSERT [ExportAses].[ConfigurationTableImport] ON 
GO 
MERGE INTO [ExportAses].[ConfigurationTableImport] AS Target 
USING (VALUES 
(1, N'NetworkProvidersImport', N'CarrierCode', 1, N'NVARCHAR', 2, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(2, N'NetworkProvidersImport', N'ProviderType', 2, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(3, N'NetworkProvidersImport', N'ReportDate', 3, N'NVARCHAR', 10, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(4, N'NetworkProvidersImport', N'PMG', 4, N'NVARCHAR', 8, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(5, N'NetworkProvidersImport', N'PMGName', 5, N'NVARCHAR', 80, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(6, N'NetworkProvidersImport', N'PMGFederalTaxId', 6, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(7, N'NetworkProvidersImport', N'NPI', 7, N'NVARCHAR', 10, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(8, N'NetworkProvidersImport', N'FederalTaxId', 8, N'NVARCHAR', 9, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(9, N'NetworkProvidersImport', N'SpecialityCode', 9, N'NVARCHAR', 2, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(10, N'NetworkProvidersImport', N'AssignedLives', 10, N'INT', NULL, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(11, N'NetworkProvidersImport', N'Name', 11, N'NVARCHAR', 80, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(12, N'NetworkProvidersImport', N'LastName1', 12, N'NVARCHAR', 30, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(13, N'NetworkProvidersImport', N'LastName2', 13, N'NVARCHAR', 30, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(14, N'NetworkProvidersImport', N'FirstName', 14, N'NVARCHAR', 50, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(15, N'NetworkProvidersImport', N'MiddleName', 15, N'NVARCHAR', 30, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(16, N'NetworkProvidersImport', N'AddressLine1', 16, N'NVARCHAR', 45, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(17, N'NetworkProvidersImport', N'AddressLine2', 17, N'NVARCHAR', 45, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(18, N'NetworkProvidersImport', N'City', 18, N'NVARCHAR', 45, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(19, N'NetworkProvidersImport', N'ZipCode', 19, N'NVARCHAR', 9, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(20, N'NetworkProvidersImport', N'Phone', 20, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(21, N'NetworkProvidersImport', N'Fax', 21, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(22, N'NetworkProvidersImport', N'Sunday', 22, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(23, N'NetworkProvidersImport', N'Monday', 23, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(24, N'NetworkProvidersImport', N'Tuesday', 24, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(25, N'NetworkProvidersImport', N'Wednesday', 25, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(26, N'NetworkProvidersImport', N'Thursday', 26, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(27, N'NetworkProvidersImport', N'Friday', 27, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(28, N'NetworkProvidersImport', N'Saturday', 28, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(29, N'NetworkProvidersImport', N'LicenseNumber', 29, N'NVARCHAR', 20, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(30, N'NetworkProvidersImport', N'ContactPerson', 30, N'NVARCHAR', 80, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(31, N'NetworkProvidersImport', N'Gender', 31, N'NVARCHAR', 2, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(32, N'NetworkProvidersImport', N'Language', 32, N'NVARCHAR', 3, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(33, N'NetworkProvidersImport', N'ProviderCapacity', 33, N'INT', NULL, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(34, N'NetworkProvidersImport', N'AcceptGender', 34, N'NVARCHAR', 2, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(35, N'NetworkProvidersImport', N'AgeAcceptBegin', 35, N'INT', NULL, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(36, N'NetworkProvidersImport', N'AgeAcceptEnd', 36, N'INT', NULL, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(37, N'NetworkProvidersImport', N'ContractStatus', 37, N'NVARCHAR', 3, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(38, N'NetworkProvidersImport', N'MunicipalityCode', 38, N'NVARCHAR', 4, N'System', GETDATE(), 1, N'PmgPcpMcoProcess'),
(39, N'NetworkProvidersImport', N'NPIPmg', 39, N'NVARCHAR', 10, N'System', GETDATE(), 1, N'PmgPcpMcoProcess')

) 
AS Source (Id, [TableName], [ColumnName], [OrdinalPosition], [DataType], [Length], [CreatedBy], [CreatedOn], [Enabled], [ProcessType])
ON Target.Id = Source.Id
WHEN MATCHED THEN 
UPDATE SET [TableName] = Source.[TableName], 
		   [ColumnName]= Source.[ColumnName],
		   [OrdinalPosition]= Source.[OrdinalPosition],
		   [DataType]= Source.[DataType],
		   [Length]= Source.[Length],
		   [CreatedBy]= Source.[CreatedBy],
		   [CreatedOn]= Source.[CreatedOn],
		   [Enabled]= Source.[Enabled],
		   [ProcessType]= Source.[ProcessType]	   
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, [TableName], [ColumnName], [OrdinalPosition], [DataType], [Length], [CreatedBy], [CreatedOn], [Enabled], [ProcessType])
VALUES (Id, [TableName], [ColumnName], [OrdinalPosition], [DataType], [Length], [CreatedBy], [CreatedOn], [Enabled], [ProcessType])
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO 
SET IDENTITY_INSERT [ExportAses].[ConfigurationTableImport] OFF 
GO 