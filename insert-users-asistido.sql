USE [EnrollmentDB2]
GO

DECLARE @cnt INT = 1;
DECLARE @cnt_total INT = 40;
DECLARE @lastId INT;

WHILE @cnt <= @cnt_total
BEGIN
	INSERT INTO [Identity].[IdentityUsers]
			   ([UserName]
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
			   ,[HasDefaultCredentials])
		 VALUES
			   (CONCAT('usuario',@cnt)
			   ,0
			   ,'AKtts4uOjMkwEWLYoREmkOBju4x19lVt8EyoojqqZvd31LKMoouhlaW+iAVc6/3Gdw=='
			   ,'bbb6d177-5048-4a98-8a92-f7eaeb0311a0'
			   ,0
			   ,0
			   ,0
			   ,0
			   ,'System'
			   ,GETDATE()
			   ,1
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
			   (2
			   ,@lastId
			   ,2
			   ,'System'
			   ,GETDATE()
			   ,1)

	SET @cnt = @cnt + 1;
END;