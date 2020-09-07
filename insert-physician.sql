USE [EnrollmentDB2]
GO

/*===============================
             STEP 0
===============================*/

UPDATE [Enrollment].[Members] 
SET [PCPId] = NULL 
WHERE [PCPId] IN (
				SELECT [Id]
				FROM [Enrollment].[PrimaryCarePhysician] 
				WHERE [PersonPrimaryCarePhysicianId] IN (SELECT [Id] 
														FROM [Enrollment].[PersonPrimaryCarePhysician] 
														WHERE [NPI]='9876543210')
				)
GO


DELETE
FROM [Enrollment].[PrimaryCarePhysicianDetail]  
WHERE [PcpPmgMcoId] IN(SELECT [Id]
					FROM [Enrollment].[PcpPmgMco]
					WHERE [PrimaryCarePhysicianId] IN (SELECT [Id] 
													 FROM [Enrollment].[PrimaryCarePhysician] 
													 WHERE [PersonPrimaryCarePhysicianId] IN (SELECT [Id] 
																							 FROM [Enrollment].[PersonPrimaryCarePhysician] 
																							 WHERE [NPI]='9876543210')
													)
					)
GO


DELETE
FROM [Enrollment].[PcpPmgMco]
WHERE [PrimaryCarePhysicianId] IN (SELECT [Id] 
									FROM [Enrollment].[PrimaryCarePhysician] 
									WHERE [PersonPrimaryCarePhysicianId] IN (SELECT [Id] 
																			FROM [Enrollment].[PersonPrimaryCarePhysician] 
																			WHERE [NPI]='9876543210')
								)
GO


DELETE
FROM [Enrollment].[PrimaryCarePhysician] 
WHERE [PersonPrimaryCarePhysicianId] IN (SELECT [Id] 
										FROM [Enrollment].[PersonPrimaryCarePhysician] 
										WHERE [NPI]='9876543210')
GO


DELETE
FROM [Enrollment].[PersonPrimaryCarePhysician] 
WHERE [NPI]='9876543210'
GO


/*===============================
             STEP 1
===============================*/

INSERT INTO [Enrollment].[PersonPrimaryCarePhysician]
           ([NPI]
           ,[FullName]
           ,[FirstName]
           ,[MiddleName]
           ,[FirstLastName]
           ,[SecondLastName]
           ,[CreatedOn]
           ,[Enabled]
           ,[GenderId])
     VALUES
           ('9876543210'
           ,'TANTAHUILCA TORRES JOSIMAR JAVIER'
           ,'JOSIMAR'
           ,'JAVIER'
           ,'TANTAHUILCA'
           ,'TORRES'
           ,GETDATE()
           ,1
           ,1)
GO

-- SELECT * FROM [Enrollment].[PersonPrimaryCarePhysician] WHERE [NPI]='9876543210'
/*===============================
             STEP 2
===============================*/

DECLARE @PersonPrimaryCarePhysicianId INT
SET @PersonPrimaryCarePhysicianId = (SELECT Id FROM [Enrollment].[PersonPrimaryCarePhysician] WHERE [NPI] = '9876543210')

INSERT INTO [Enrollment].[PrimaryCarePhysician]
           ([PersonPrimaryCarePhysicianId]
           ,[SpecialityId]
           ,[CreatedOn]
           ,[Enabled])
     VALUES
           (@PersonPrimaryCarePhysicianId
           ,1
           ,GETDATE()
           ,1)

INSERT INTO [Enrollment].[PrimaryCarePhysician]
           ([PersonPrimaryCarePhysicianId]
           ,[SpecialityId]
           ,[CreatedOn]
           ,[Enabled])
     VALUES
           (@PersonPrimaryCarePhysicianId
           ,37
           ,GETDATE()
           ,1)
GO

-- SELECT * FROM [Enrollment].[PrimaryCarePhysician] WHERE [PersonPrimaryCarePhysicianId] = (SELECT [Id] FROM [Enrollment].[PersonPrimaryCarePhysician] WHERE [NPI]='9876543210')
/*===============================
             STEP 3-4
===============================*/


DECLARE 
    @PrimaryCarePhysicianId INT,
	@PersonPrimaryCarePhysicianId INT,
	@rowNum INT;

SET @PersonPrimaryCarePhysicianId = (SELECT Id FROM [Enrollment].[PersonPrimaryCarePhysician] WHERE [NPI] = '9876543210')
 
DECLARE cursor_PrimaryCarePhysician CURSOR
FOR SELECT
		Id, ROW_NUMBER() OVER(ORDER BY Id ASC) AS rowNum
	FROM
		[Enrollment].[PrimaryCarePhysician]
	WHERE
		[PersonPrimaryCarePhysicianId] = @PersonPrimaryCarePhysicianId
 
OPEN cursor_PrimaryCarePhysician;
 
FETCH NEXT FROM cursor_PrimaryCarePhysician INTO 
    @PrimaryCarePhysicianId, @rowNum;
 
WHILE @@FETCH_STATUS = 0
    BEGIN
		DECLARE @PmgId1 INT, @PmgId2 INT, @PmgId3 INT, @PmgId4 INT;
		DECLARE @PrimaryMedicalGroupId INT;
		--SET @PmgId1 = @rowNum * 10 + 1;
		--SET @PmgId2 = @rowNum * 10 + 2;
		--SET @PmgId3 = @rowNum * 10 + 3;
		--SET @PmgId4 = @rowNum * 10 + 4;
		SET @PrimaryMedicalGroupId = (SELECT TOP 1 [Id] FROM [Enrollment].[PrimaryMedicalGroup] ORDER BY [Id]);
		SET @PmgId1 = @rowNum * 10 + @PrimaryMedicalGroupId + 1;
		SET @PmgId2 = @rowNum * 10 + @PrimaryMedicalGroupId + 2;
		SET @PmgId3 = @rowNum * 10 + @PrimaryMedicalGroupId + 3;
		SET @PmgId4 = @rowNum * 10 + @PrimaryMedicalGroupId + 4;

		INSERT INTO [Enrollment].[PcpPmgMco]
				   ([PrimaryCarePhysicianId]
				   ,[PmgId]
				   ,[McoId]
				   ,[CreatedOn]
				   ,[Enabled])
			 VALUES
				   (@PrimaryCarePhysicianId
				   ,@PmgId1
				   ,1
				   ,GETDATE()
				   ,1)
		INSERT INTO [Enrollment].[PcpPmgMco]
				   ([PrimaryCarePhysicianId]
				   ,[PmgId]
				   ,[McoId]
				   ,[CreatedOn]
				   ,[Enabled])
			 VALUES
				   (@PrimaryCarePhysicianId
				   ,@PmgId2
				   ,4
				   ,GETDATE()
				   ,1)
		INSERT INTO [Enrollment].[PcpPmgMco]
				   ([PrimaryCarePhysicianId]
				   ,[PmgId]
				   ,[McoId]
				   ,[CreatedOn]
				   ,[Enabled])
			 VALUES
				   (@PrimaryCarePhysicianId
				   ,@PmgId3
				   ,5
				   ,GETDATE()
				   ,1)
		INSERT INTO [Enrollment].[PcpPmgMco]
				   ([PrimaryCarePhysicianId]
				   ,[PmgId]
				   ,[McoId]
				   ,[CreatedOn]
				   ,[Enabled])
			 VALUES
				   (@PrimaryCarePhysicianId
				   ,@PmgId4
				   ,5
				   ,GETDATE()
				   ,1)
        
		FETCH NEXT FROM cursor_PrimaryCarePhysician INTO 
            @PrimaryCarePhysicianId, @rowNum;
    END;
 
CLOSE cursor_PrimaryCarePhysician;
 
DEALLOCATE cursor_PrimaryCarePhysician;

GO


/*===============================
             STEP 5
===============================*/

DECLARE 
	@Id INT,
	@rowNum  INT;

DECLARE cursor_PrimaryCarePhysician CURSOR
FOR SELECT
		Id, ROW_NUMBER() OVER(ORDER BY Id ASC) AS rowNum
	FROM
		[Enrollment].[PcpPmgMco]
	WHERE
		[PrimaryCarePhysicianId] IN (
			SELECT Id FROM [Enrollment].[PrimaryCarePhysician] WHERE [PersonPrimaryCarePhysicianId] = (
				SELECT Id FROM [Enrollment].[PersonPrimaryCarePhysician] WHERE [NPI] = '9876543210'
			)
		)
 
OPEN cursor_PrimaryCarePhysician;
 
FETCH NEXT FROM cursor_PrimaryCarePhysician INTO 
    @Id, @rowNum;
 
WHILE @@FETCH_STATUS = 0
    BEGIN

		INSERT INTO [Enrollment].[PrimaryCarePhysicianDetail]
				   ([Phone]
				   ,[Enabled]
				   ,[AddressLineOne]
				   ,[AddressLineTwo]
				   ,[City]
				   ,[ZipCode]
				   ,[State]
				   ,[PcpPmgMcoId]
				   ,[MunicipalityId])
			 VALUES
				   ('9876543210'
				   ,1
				   ,CONCAT('Linea 1 ', @Id, ' #1')
				   ,CONCAT('Linea 2 ', @Id, ' #1')
				   ,CONCAT('City ', @Id)
				   ,'00123'
				   ,'PR'
				   ,@Id
				   ,@rowNum)

		INSERT INTO [Enrollment].[PrimaryCarePhysicianDetail]
				   ([Phone]
				   ,[Enabled]
				   ,[AddressLineOne]
				   ,[AddressLineTwo]
				   ,[City]
				   ,[ZipCode]
				   ,[State]
				   ,[PcpPmgMcoId]
				   ,[MunicipalityId])
			 VALUES
				   ('1231231231'
				   ,1
				   ,CONCAT('Linea 1 ', @Id, ' #2')
				   ,CONCAT('Linea 2 ', @Id, ' #2')
				   ,CONCAT('City ', @Id)
				   ,'00321'
				   ,'PR'
				   ,@Id
				   ,@rowNum)

		INSERT INTO [Enrollment].[PrimaryCarePhysicianDetail]
				   ([Phone]
				   ,[Enabled]
				   ,[AddressLineOne]
				   ,[AddressLineTwo]
				   ,[City]
				   ,[ZipCode]
				   ,[State]
				   ,[PcpPmgMcoId]
				   ,[MunicipalityId])
			 VALUES
				   ('1234567890'
				   ,1
				   ,CONCAT('Linea 1 ', @Id, ' #3')
				   ,CONCAT('Linea 2 ', @Id, ' #3')
				   ,CONCAT('City ', @Id)
				   ,'00456'
				   ,'PR'
				   ,@Id
				   ,@rowNum)
        
		FETCH NEXT FROM cursor_PrimaryCarePhysician INTO 
            @Id, @RowNum;
    END;
 
CLOSE cursor_PrimaryCarePhysician;
 
DEALLOCATE cursor_PrimaryCarePhysician;

GO