 --DECLARE
 -- @Csv_FilePath VARCHAR(300)='D:\TrueNorthCorporation\ProyEnrollment\DownloadfromSFTP\CSVInput_06252020_195840_1.csv',    
 --@Excel_FileName VARCHAR(100)='/CAVV/Npl Short Layout (Providers) - Plus 20200521.xlsx',    
 --@ProcessType VARCHAR(20)='cargar excel',    
 --@UserName VARCHAR(30)='manual'    
CREATE PROCEDURE [ExportAses].[USP_UploadDataExcel]     
 @Csv_FilePath VARCHAR(300),    
 @Excel_FileName VARCHAR(100),    
 @ProcessType VARCHAR(20),    
 @UserName VARCHAR(30)    
AS    
BEGIN     

 DECLARE @SqlStatement VARCHAR(500)    
 DECLARE @IdInserted INT    
 DECLARE @Procedure VARCHAR(100)    
 DECLARE @Rows int    
 DECLARE @Error VARCHAR(300)    
 DECLARE @Start DATETIME    
 DECLARE @End DATETIME    
 CREATE TABLE #TEMP (    
  Carrier_Code VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Provider_Type VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  ReportDate VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  PMG VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  PMG_Name VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  PMG_federal_tax_id VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  NPI VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Federal_Tax_ID VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Specialty_Code VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Assigned_Lives VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Name VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Last_Name1 VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Last_Name2 VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  First_Name VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  MI VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Addr1 VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Addr2 VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  City VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Zip VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Phone VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Fax VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Sunday VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Monday VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Tuesday VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Wednesday VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Thursday VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Friday VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Saturday VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  License_Num VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Contact_Person VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Gender VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Language VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Provider_Capacity VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Accept_Gender VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Age_Accept_Begin VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Age_Accept_End VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  Contract_Status VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  municipality_code VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI,    
  NPI_PMG VARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI    
 )    
 DECLARE @TransactionName VARCHAR(20) = 'PROCESS_TRANSACTION_UPLOAD_EXCEL'    
    
 BEGIN TRY    
  SET @Procedure = OBJECT_NAME(@@PROCID)    
  SET @Start = GETDATE()    
  SET @SqlStatement = 'BULK INSERT #TEMP FROM ''' + @Csv_FilePath +     
  ''' WITH(FIELDTERMINATOR = ''' + '|' + ''', CHECK_CONSTRAINTS, CODEPAGE = ''' + 'ACP' + ''', DATAFILETYPE = ''' + 'WIDECHAR' + ''')'    
  EXECUTE (@SqlStatement)    


  BEGIN TRAN @TransactionName    
  INSERT INTO [ExportAses].[ProcessHeader](    
   FileName,    
   ProcessType,    
   StartTime,    
   EndTime,    
   CreatedBy,    
   CreatedOn,    
   Enabled,    
   Exception    
  ) VALUES(    
   @Excel_FileName,    
   @ProcessType,    
   @Start,    
   NULL,    
   @UserName,    
   CONVERT(DATE, GETDATE()),    
   1,    
   NULL    
  )    
  SET @IdInserted = @@IDENTITY    
  INSERT INTO [ExportAses].[NetworkProvidersImport] (    
   CarrierCode,    
   ProviderType,    
   ReportDate,    
   PMG,    
   PMGName,    
   PMGFederalTaxId,    
  NPI,    
   FederalTaxId,    
   SpecialityCode,    
   AssignedLives,    
   Name,    
   LastName1,    
   LastName2,    
   FirstName,    
   MiddleName,    
   AddressLine1,    
   AddressLine2,    
   City,    
   ZipCode,    
   Phone,    
   Fax,    
   Sunday,    
   Monday,    
   Tuesday,    
   Wednesday,    
   Thursday,    
   Friday,    
   Saturday,    
   LicenseNumber,    
   ContactPerson,    
   Gender,    
   Language,    
   ProviderCapacity,    
   AcceptGender,    
   AgeAcceptBegin,    
   AgeAcceptEnd,    
   ContractStatus,    
   MunicipalityCode,    
   NPIPmg,    
   ProcessHeaderId,    
   IsValidForImport,    
   MessageInvalid    
  )    
  SELECT    
   A.Carrier_Code_Column,    
   A.Provider_Type_Column,    
   A.ReportDate_Column,    
   A.PMG_Column,    
   A.PMG_Name_Column,    
   A.PMG_federal_tax_id_Column,    
   A.NPI_Column,    
   A.Federal_Tax_ID_Column,    
   A.Specialty_Code_Column,    
   A.Assigned_Lives_Column,    
   A.Name_Column,    
   A.Last_Name1_Column,    
   A.Last_Name2_Column,    
   A.First_Name_Column,    
   A.MI_Column,    
   A.Addr1_Column,    
   A.Addr2_Column,    
   A.City_Column,    
   A.Zip_Column,    
   A.Phone_Column,    
   A.Fax_Column,    
   A.Sunday_Column,    
   A.Monday_Column,    
   A.Tuesday_Column,    
   A.Wednesday_Column,    
   A.Thursday_Column,    
   A.Friday_Column,    
   A.Saturday_Column,    
   A.License_Num_Column,    
   A.Contact_Person_Column,    
   A.Gender_Column,    
   A.Language_Column,    
   A.Provider_Capacity_Column,    
   A.Accept_Gender_Column,    
   A.Age_Accept_Begin_Column,    
   A.Age_Accept_End_Column,    
   A.Contract_Status_Column,    
   A.municipality_code_Column,    
   A.NPI_PMG_Column,    
   A.ID,
   cast(1 as bit),
   NULL
   --CASE WHEN (Validation1 + Validation2 + Validation3 + Validation4 + Validation5 + Validation6 + Validation7 + Validation8 + Validation9 + Validation10 + Validation11 + Validation12 + Validation13 + Validation14 + Validation15 + Validation16 + Validation17 + Validation18 + Validation19 + Validation20 + Validation21 + Validation22 + Validation23 + Validation24 + Validation25 + Validation26 + Validation27 + Validation28 + Validation29 + Validation30 + Validation31 + Validation32 + Validation33 + Validation34 + Validation35 + Validation36 + Validation37 + Validation38 + Validation39 > 0) THEN 0 ELSE 1 END,    
   --CASE WHEN (Validation1 + Validation2 + Validation3 + Validation4 + Validation5 + Validation6 + Validation7 + Validation8 + Validation9 + Validation10 + Validation11 + Validation12 + Validation13 + Validation14 + Validation15 + Validation16 + Validation17 + Validation18 + Validation19 + Validation20 + Validation21 + Validation22 + Validation23 + Validation24 + Validation25 + Validation26 + Validation27 + Validation28 + Validation29 + Validation30 + Validation31 + Validation32 + Validation33 + Validation34 + Validation35 + Validation36 + Validation37 + Validation38 + Validation39 > 0)    
   -- THEN CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(Message1 , Message2 ), Message3 ), Message4 ), Message5 ), Message6 ), Message7 ), Message8 ), Message9 ), Message10 ), Message11 ), Message12 ), Message13 ), Message14 ), Message15 ), Message16 ), Message17 ), Message18 ), Message19 ), Message20 ), Message21 ), Message22 ), Message23 ), Message24 ), Message25 ), Message26 ), Message27 ), Message28 ), Message29 ), Message30 ), Message31 ), Message32 ), Message33 ), Message34 ), Message35 ), Message36 ), Message37 ), Message38 ), Message39), '|')    
   -- ELSE NULL    
   --END    
    
   FROM (    
    SELECT     
     LTRIM(RTRIM(Carrier_Code)) AS Carrier_Code_Column,    
     LTRIM(RTRIM(Provider_Type)) AS Provider_Type_Column,    
     LTRIM(RTRIM(ReportDate)) AS ReportDate_Column,    
     LTRIM(RTRIM(PMG)) AS PMG_Column,    
     LTRIM(RTRIM(PMG_Name)) AS PMG_Name_Column,    
     LTRIM(RTRIM(replace(ltrim(replace(PMG_federal_tax_id,'0',' ')),' ','0'))) AS PMG_federal_tax_id_Column,    
     LTRIM(RTRIM(NPI)) AS NPI_Column,    
     LTRIM(RTRIM(Federal_Tax_ID)) AS Federal_Tax_ID_Column,    
     LTRIM(RTRIM(Specialty_Code)) AS Specialty_Code_Column,    
     LTRIM(RTRIM(Assigned_Lives)) AS Assigned_Lives_Column,    
     LTRIM(RTRIM(Name)) AS Name_Column,    
     LTRIM(RTRIM(Last_Name1)) AS Last_Name1_Column,    
     LTRIM(RTRIM(Last_Name2)) AS Last_Name2_Column,    
     LTRIM(RTRIM(First_Name)) AS First_Name_Column,    
     LTRIM(RTRIM(MI)) AS MI_Column,    
     LTRIM(RTRIM(Addr1)) AS Addr1_Column,    
     LTRIM(RTRIM(Addr2)) AS Addr2_Column,    
     LTRIM(RTRIM(City)) AS City_Column,    
     LTRIM(RTRIM(Zip)) AS Zip_Column,    
     LTRIM(RTRIM(Phone)) AS Phone_Column,    
     LTRIM(RTRIM(Fax)) AS Fax_Column,    
     LTRIM(RTRIM(Sunday)) AS Sunday_Column,    
     LTRIM(RTRIM(Monday)) AS Monday_Column,    
     LTRIM(RTRIM(Tuesday)) AS Tuesday_Column,    
     LTRIM(RTRIM(Wednesday)) AS Wednesday_Column,    
     LTRIM(RTRIM(Thursday)) AS Thursday_Column,    
     LTRIM(RTRIM(Friday)) AS Friday_Column,    
     LTRIM(RTRIM(Saturday)) AS Saturday_Column,    
     LTRIM(RTRIM(License_Num)) AS License_Num_Column,    
     LTRIM(RTRIM(Contact_Person)) AS Contact_Person_Column,    
     LTRIM(RTRIM(Gender)) AS Gender_Column,    
     LTRIM(RTRIM(Language)) AS Language_Column,    
     LTRIM(RTRIM(Provider_Capacity)) AS Provider_Capacity_Column,    
     LTRIM(RTRIM(Accept_Gender)) AS Accept_Gender_Column,    
     LTRIM(RTRIM(Age_Accept_Begin)) AS Age_Accept_Begin_Column,    
     LTRIM(RTRIM(Age_Accept_End)) AS Age_Accept_End_Column,    
     LTRIM(RTRIM(Contract_Status)) AS Contract_Status_Column,    
     LTRIM(RTRIM(municipality_code)) AS municipality_code_Column,    
     LTRIM(RTRIM(NPI_PMG)) AS NPI_PMG_Column,    
     @IdInserted AS ID,    
     --FOR COLUMN IsValidForImport    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(Carrier_Code, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 1 AND Enabled = 1)) THEN 1
	 	  WHEN Carrier_Code IS NULL THEN 1
		  WHEN LTRIM(RTRIM(Carrier_Code)) = '0' THEN 1
		  ELSE 0 END AS Validation1, 
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(Provider_Type, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 2 AND Enabled = 1)) THEN 1
	 	  WHEN Provider_Type IS NULL THEN 1
		  WHEN LTRIM(RTRIM(Provider_Type)) = '0' THEN 1
		  ELSE 0 END AS Validation2,
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(ReportDate, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 3 AND Enabled = 1)) THEN 1 
	 	  WHEN ReportDate IS NULL THEN 1
		  WHEN LTRIM(RTRIM(ReportDate)) = '0' THEN 1
		  ELSE 0 END AS Validation3,  
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(PMG, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 4 AND Enabled = 1)) THEN 1 
	 	  WHEN PMG IS NULL THEN 1
		  WHEN LTRIM(RTRIM(PMG)) = '0' THEN 1
		  ELSE 0 END AS Validation4,  
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(PMG_Name, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 5 AND Enabled = 1)) THEN 1 
	 	  WHEN PMG_Name IS NULL THEN 1
		  WHEN LTRIM(RTRIM(PMG_Name)) = '0' THEN 1
		  ELSE 0 END AS Validation5,    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(replace(ltrim(replace(PMG_federal_tax_id,'0',' ')),' ','0'), '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 6 AND Enabled = 1)) THEN 1 
	 	  WHEN PMG_federal_tax_id IS NULL THEN 1
		  WHEN LTRIM(RTRIM(replace(ltrim(replace(PMG_federal_tax_id,'0',' ')),' ','0'))) = '0' THEN 1
		  ELSE 0 END AS Validation6,    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(NPI, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 7 AND Enabled = 1)) THEN 1 
	 	  WHEN NPI IS NULL THEN 1
		  WHEN LTRIM(RTRIM(NPI)) = '0' THEN 1
		  ELSE 0 END AS Validation7,    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(Federal_Tax_ID, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 8 AND Enabled = 1)) THEN 1 
		  WHEN Federal_Tax_ID IS NULL THEN 1
		  WHEN LTRIM(RTRIM(Federal_Tax_ID)) = '0' THEN 1
		  ELSE 0 END AS Validation8,    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(Specialty_Code, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 9 AND Enabled = 1)) THEN 1 
	 	  WHEN Specialty_Code IS NULL THEN 1
		  WHEN LTRIM(RTRIM(Specialty_Code)) = '0' THEN 1
		  ELSE 0 END AS Validation9,    
     CASE WHEN (ISNUMERIC(Assigned_Lives) = 0) THEN 1 ELSE 0 END AS Validation10,    
     CASE WHEN (LEN(LTRIM(RTRIM(Name))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 11 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation11, 
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(Last_Name1, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 12 AND Enabled = 1)) THEN 1 
	 	  WHEN Last_Name1 IS NULL THEN 1
		  WHEN LTRIM(RTRIM(Last_Name1)) = '0' THEN 1
		  ELSE 0 END AS Validation12, 
     CASE WHEN (LEN(LTRIM(RTRIM(Last_Name2))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 13 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation13, 
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(First_Name, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 14 AND Enabled = 1)) THEN 1 
	 	  WHEN First_Name IS NULL THEN 1
		  WHEN LTRIM(RTRIM(First_Name)) = '0' THEN 1
		  ELSE 0 END AS Validation14, 
     CASE WHEN (LEN(LTRIM(RTRIM(MI))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 15 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation15,    
     CASE WHEN (LEN(LTRIM(RTRIM(Addr1))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 16 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation16,    
     CASE WHEN (LEN(LTRIM(RTRIM(Addr2))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 17 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation17,    
     CASE WHEN (LEN(LTRIM(RTRIM(City))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 18 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation18,    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(Zip, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 19 AND Enabled = 1)) THEN 1 
	 	  WHEN Zip IS NULL THEN 1
		  WHEN LTRIM(RTRIM(Zip)) = '0' THEN 1
		  ELSE 0 END AS Validation19,    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(Phone, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 20 AND Enabled = 1)) THEN 1 
	 	  WHEN Phone IS NULL THEN 1
		  WHEN LTRIM(RTRIM(Phone)) = '0' THEN 1
		  ELSE 0 END AS Validation20,    
     CASE WHEN (LEN(LTRIM(RTRIM(Fax))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 21 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation21,    
     CASE WHEN (LEN(LTRIM(RTRIM(Sunday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 22 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation22,    
     CASE WHEN (LEN(LTRIM(RTRIM(Monday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 23 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation23,    
     CASE WHEN (LEN(LTRIM(RTRIM(Tuesday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 24 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation24,    
     CASE WHEN (LEN(LTRIM(RTRIM(Wednesday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 25 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation25,  
     CASE WHEN (LEN(LTRIM(RTRIM(Thursday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 26 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation26,   
     CASE WHEN (LEN(LTRIM(RTRIM(Friday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 27 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation27,    
     CASE WHEN (LEN(LTRIM(RTRIM(Saturday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 28 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation28,   
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(License_Num, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 29 AND Enabled = 1)) THEN 1 
	 	  WHEN License_Num IS NULL THEN 1
		  WHEN LTRIM(RTRIM(License_Num)) = '0' THEN 1
		  ELSE 0 END AS Validation29,
     CASE WHEN (LEN(LTRIM(RTRIM(Contact_Person))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 30 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation30,    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(Gender, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 31 AND Enabled = 1)) THEN 1 
		  WHEN Gender IS NOT NULL AND LTRIM(RTRIM(Gender)) NOT IN ('F', 'M') THEN 1
		  ELSE 0 END AS Validation31,    
     CASE WHEN (LEN(LTRIM(RTRIM(Language))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 32 AND Enabled = 1)) THEN 1 ELSE 0 END AS Validation32,   
     CASE WHEN (ISNUMERIC(Provider_Capacity) = 0) THEN 1 ELSE 0 END AS Validation33,    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(Accept_Gender, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 34 AND Enabled = 1)) THEN 1 
	 	  WHEN Accept_Gender IS NULL THEN 1
		  WHEN LTRIM(RTRIM(Accept_Gender)) = '0' THEN 1
		  ELSE 0 END AS Validation34,    
     CASE WHEN (ISNUMERIC(Age_Accept_Begin) = 0) THEN 1 ELSE 0 END AS Validation35,    
     CASE WHEN (ISNUMERIC(Age_Accept_End) = 0) THEN 1 ELSE 0 END AS Validation36,    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(Contract_Status, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 37 AND Enabled = 1)) THEN 1 
	 	  WHEN Contract_Status IS NULL THEN 1
		  WHEN LTRIM(RTRIM(Contract_Status)) = '0' THEN 1
		  ELSE 0 END AS Validation37,    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(municipality_code, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 38 AND Enabled = 1)) THEN 1 
	 	  WHEN municipality_code IS NULL THEN 1
		  WHEN LTRIM(RTRIM(municipality_code)) = '0' THEN 1
		  ELSE 0 END AS Validation38,    
     CASE WHEN (LEN(LTRIM(RTRIM(COALESCE(NPI_PMG, '')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 39 AND Enabled = 1)) THEN 1 
	 	  WHEN NPI_PMG IS NULL THEN 1
		  WHEN LTRIM(RTRIM(NPI_PMG)) = '0' THEN 1
		  ELSE 0 END AS Validation39,    
     
	 --FOR COLUMN MessageInvalid    
     CASE WHEN (LEN(LTRIM(RTRIM(Carrier_Code))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 1 AND Enabled = 1)) THEN '|Column CarrierCode have more characters than allowed.' END AS Message1,    
     CASE WHEN (LEN(LTRIM(RTRIM(Provider_Type))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 2 AND Enabled = 1)) THEN '|Column ProviderType have more characters than allowed.' END AS Message2,    
     CASE WHEN (LEN(LTRIM(RTRIM(ReportDate))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 3 AND Enabled = 1)) THEN '|Column ReportDate have more characters than allowed.' END AS Message3,    
     CASE WHEN (LEN(LTRIM(RTRIM(PMG))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 4 AND Enabled = 1)) THEN '|Column PMG have more characters than allowed.' END AS Message4,    
     CASE WHEN (LEN(LTRIM(RTRIM(PMG_Name))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 5 AND Enabled = 1)) THEN '|Column PMGName have more characters than allowed.' END AS Message5,    
     CASE WHEN (LEN(LTRIM(RTRIM(replace(ltrim(replace(PMG_federal_tax_id,'0',' ')),' ','0')))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 6 AND Enabled = 1)) THEN '|Column PMGFederalTaxId have more characters than allowed.' END AS Message6,    
     CASE WHEN (LEN(LTRIM(RTRIM(NPI))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 7 AND Enabled = 1)) THEN '|Column NPI have more characters than allowed.' END AS Message7,    
     CASE WHEN (LEN(LTRIM(RTRIM(Federal_Tax_ID))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 8 AND Enabled = 1)) THEN '|Column FederalTaxId have more characters than allowed.' END AS Message8,    
     CASE WHEN (LEN(LTRIM(RTRIM(Specialty_Code))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 9 AND Enabled = 1)) THEN '|Column SpecialityCode have more characters than allowed.' END AS Message9,    
     CASE WHEN (ISNUMERIC(Assigned_Lives) = 0) THEN '|Column AssignedLives is not numeric.' END AS Message10,    
     CASE WHEN (LEN(LTRIM(RTRIM(Name))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 11 AND Enabled = 1)) THEN '|Column Name have more characters than allowed.' END AS Message11,    
     CASE WHEN (LEN(LTRIM(RTRIM(Last_Name1))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 12 AND Enabled = 1)) THEN '|Column LastName1 have more characters than allowed.' END AS Message12,    
     CASE WHEN (LEN(LTRIM(RTRIM(Last_Name2))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 13 AND Enabled = 1)) THEN '|Column LastName2 have more characters than allowed.' END AS Message13,    
     CASE WHEN (LEN(LTRIM(RTRIM(First_Name))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 14 AND Enabled = 1)) THEN '|Column FirstName have more characters than allowed.' END AS Message14,    
     CASE WHEN (LEN(LTRIM(RTRIM(MI))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 15 AND Enabled = 1)) THEN '|Column MiddleName have more characters than allowed.' END AS Message15,    
     CASE WHEN (LEN(LTRIM(RTRIM(Addr1))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 16 AND Enabled = 1)) THEN '|Column AddressLine1 have more characters than allowed.' END AS Message16,    
     CASE WHEN (LEN(LTRIM(RTRIM(Addr2))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 17 AND Enabled = 1)) THEN '|Column AddressLine2 have more characters than allowed.' END AS Message17,    
     CASE WHEN (LEN(LTRIM(RTRIM(City))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 18 AND Enabled = 1)) THEN '|Column City have more characters than allowed.' END AS Message18,    
     CASE WHEN (LEN(LTRIM(RTRIM(Zip))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 19 AND Enabled = 1)) THEN '|Column ZipCode have more characters than allowed.' END AS Message19,    
     CASE WHEN (LEN(LTRIM(RTRIM(Phone))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 20 AND Enabled = 1)) THEN '|Column Phone have more characters than allowed.' END AS Message20,    
     CASE WHEN (LEN(LTRIM(RTRIM(Fax))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 21 AND Enabled = 1)) THEN '|Column Fax have more characters than allowed.' END AS Message21,    
     CASE WHEN (LEN(LTRIM(RTRIM(Sunday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 22 AND Enabled = 1)) THEN '|Column Sunday have more characters than allowed.' END AS Message22,    
     CASE WHEN (LEN(LTRIM(RTRIM(Monday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 23 AND Enabled = 1)) THEN '|Column Monday have more characters than allowed.' END AS Message23,    
     CASE WHEN (LEN(LTRIM(RTRIM(Tuesday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 24 AND Enabled = 1)) THEN '|Column Tuesday have more characters than allowed.' END AS Message24,    
     CASE WHEN (LEN(LTRIM(RTRIM(Wednesday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 25 AND Enabled = 1)) THEN '|Column Wednesday have more characters than allowed.' END AS Message25,    
     CASE WHEN (LEN(LTRIM(RTRIM(Thursday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 26 AND Enabled = 1)) THEN '|Column Thursday have more characters than allowed.' END AS Message26,    
     CASE WHEN (LEN(LTRIM(RTRIM(Friday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 27 AND Enabled = 1)) THEN '|Column Friday have more characters than allowed.' END AS Message27,    
     CASE WHEN (LEN(LTRIM(RTRIM(Saturday))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 28 AND Enabled = 1)) THEN '|Column Saturday have more characters than allowed.' END AS Message28,    
     CASE WHEN (LEN(LTRIM(RTRIM(License_Num))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 29 AND Enabled = 1)) THEN '|Column LicenseNumber have more characters than allowed.' END AS Message29,    
     CASE WHEN (LEN(LTRIM(RTRIM(Contact_Person))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 30 AND Enabled = 1)) THEN '|Column ContactPerson have more characters than allowed.' END AS Message30,    
     CASE WHEN (LEN(LTRIM(RTRIM(Gender))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 31 AND Enabled = 1)) THEN '|Column Gender have more characters than allowed.' END AS Message31, -- ELSE (CASE WHEN (SELECT COUNT(*) FROM Enrollment.Genders WHERE Code COLLATE SQL_Latin1_General_CP1_CI_AI = LTRIM(RTRIM(Gender))) = 0 THEN '|Data not found in table Gender.' END) END AS Message31,    
     CASE WHEN (LEN(LTRIM(RTRIM(Language))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 32 AND Enabled = 1)) THEN '|Column Language have more characters than allowed.' END AS Message32,    
     CASE WHEN (ISNUMERIC(Provider_Capacity) = 0) THEN '|Column ProviderCapacity is not numeric.' END AS Message33,    
     CASE WHEN (LEN(LTRIM(RTRIM(Accept_Gender))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 34 AND Enabled = 1)) THEN '|Column AcceptGender have more characters than allowed.' END AS Message34,    
     CASE WHEN (ISNUMERIC(Age_Accept_Begin) = 0) THEN '|Column AgeAcceptBegin is not numeric.' END AS Message35,    
     CASE WHEN (ISNUMERIC(Age_Accept_End) = 0) THEN '|Column AgeAcceptEnd is not numeric.' END AS Message36,    
     CASE WHEN (LEN(LTRIM(RTRIM(Contract_Status))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 37 AND Enabled = 1)) THEN '|Column ContractStatus have more characters than allowed.' END AS Message37,    
     CASE WHEN (LEN(LTRIM(RTRIM(municipality_code))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 38 AND Enabled = 1)) THEN '|Column MunicipalityCode have more characters than allowed.' END AS Message38,    
     CASE WHEN (LEN(LTRIM(RTRIM(NPI_PMG))) > (SELECT CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = 'NetworkProvidersImport' AND OrdinalPosition = 39 AND Enabled = 1)) THEN '|Column NPIPmg have more characters than allowed.' END AS Message39    
     
    
    FROM #TEMP    
    WHERE LTRIM(RTRIM(Carrier_Code)) <> 'Carrier_Code'    
    ) A    
  SET @Rows = @@ROWCOUNT    
  SET @Error = @@ERROR    
    
  UPDATE [ExportAses].[ProcessHeader]    
  SET EndTime = GETDATE()    
  WHERE ID = @IdInserted    
    
  COMMIT TRAN @TransactionName    
    
  SELECT    
   CAST(@IdInserted as varchar(10)) AS ProcessHeaderId    
  ,@ProcessType AS ProcessType    
  ,@Error AS ErrorNumber    
  ,@Procedure AS ProcedureName    
  ,NULL AS ErrorLine    
  ,CAST(@Rows as varchar(10)) + ' Affected Rows' AS ProcessMessage    
 END TRY      
 BEGIN CATCH    
  ROLLBACK TRAN @TransactionName    
  SELECT    
   NULL AS ProcessHeaderId    
   ,@ProcessType AS ProcessType    
  ,ERROR_NUMBER() AS ErrorNumber    
  ,ERROR_PROCEDURE() AS ProcedureName    
  ,ERROR_LINE() AS ErrorLine    
  ,ERROR_MESSAGE() AS ProcessMessage    
 END CATCH    
 IF(OBJECT_ID('EnrollmentDB.#TEMP') Is Not Null)    
 BEGIN    
  DROP TABLE #TEMP    
 END     
END