USE [EnrollmentDB2]
GO

DECLARE  @lastId INT
		,@memberId INT
		,@familyId INT
		,@last4ssn NVARCHAR(100)
		,@fln NVARCHAR(100)
		,@sln NVARCHAR(100)
		,@fn NVARCHAR(100)
		,@birth DATE
		,@zip NVARCHAR(100)
		,@email NVARCHAR(100) = 'usuario1@truenorth.pr'



SELECT TOP 1 
@memberId=[m].[Id]
,@familyId=[m].[FamilyId]
,@last4ssn=[m].[Last4SSN]
,@fln=[m].[FirstLastName]
,@sln=[m].[SecondLastName]
,@fn=[m].[FirstName]
,@birth=[m].[DateOfBirth]
,@zip=[f].[MailAddressZip4]
FROM [Enrollment].[Members] AS [m]
LEFT JOIN [Enrollment].[Families] AS [f]
ON [f].[Id] = [m].[FamilyId]
WHERE [m].[Id] NOT IN (SELECT [MemberId] FROM [Identity].[IdentityUsers] WHERE [MemberId] IS NOT NULL)
	AND [m].[FirstLastName] IS NOT NULL
	AND [m].[SecondLastName] IS NOT NULL
	AND [m].[MCOId] IS NOT NULL
	AND [m].[PCPId] IS NOT NULL
	AND [m].[PMGId] IS NOT NULL


INSERT INTO [Identity].[IdentityUsers]
           ([UserName]
           ,[FirstName]
           ,[LastName1]
           ,[LastName2]
           ,[SSNLast4]
           ,[ZipCode]
           ,[DateOfBirth]
           ,[Email]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[Enabled]
           ,[MemberId]
           ,[HasDefaultCredentials])
		VALUES
			(@email
			,@fn
			,@fln
			,@sln
			,@last4ssn
			,@zip
			,@birth
			,@email
			,0
			,'AKtts4uOjMkwEWLYoREmkOBju4x19lVt8EyoojqqZvd31LKMoouhlaW+iAVc6/3Gdw=='
			,'54d0b1ad-14dc-4054-982a-03db07149404'
			,0
			,0
			,0
			,0
			,'System'
			,GETDATE()
			,1
			,@memberId
			,0)

SET @lastId = SCOPE_IDENTITY(); 

INSERT INTO [Identity].[IdentityRolUsers]
			([ApplicationId]
			,[UserId]
			,[RoleId]
			,[CreatedBy]
			,[CreatedOn]
			,[Enabled])
		VALUES
			(1
			,@lastId
			,3
			,'System'
			,GETDATE()
			,1)

INSERT INTO [Identity].[IdentityRolUsers]
			([ApplicationId]
			,[UserId]
			,[RoleId]
			,[CreatedBy]
			,[CreatedOn]
			,[Enabled])
		VALUES
			(0
			,@lastId
			,1
			,'System'
			,GETDATE()
			,1)

GO