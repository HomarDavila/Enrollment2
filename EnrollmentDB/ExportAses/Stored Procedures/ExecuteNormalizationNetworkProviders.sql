--declare         
-- @ProcessId int = 112,           
-- @ProcessType VARCHAR(20) = 'normalizar',          
-- @UserName VARCHAR(30)  = 'manual'        
        
CREATE PROCEDURE [ExportAses].[ExecuteNormalizationNetworkProviders]
@ProcessId int, @ProcessType varchar(20), @UserName varchar(30)
WITH EXEC AS CALLER
AS
BEGIN                
 SET NOCOUNT ON;          
 DECLARE @SqlStatement VARCHAR(500)          
 DECLARE @IdInserted INT          
 DECLARE @Procedure VARCHAR(100)          
 DECLARE @Rows int          
 DECLARE @Error VARCHAR(300)          
 DECLARE @Start DATETIME          
 DECLARE @End DATETIME          
 DECLARE @TransactionName VARCHAR(20) = 'PROCESS_TRANSACTION'          
 DECLARE @trancount int          
 BEGIN TRY          
  SET @Procedure = OBJECT_NAME(@@PROCID)          
  SET @Start = GETDATE()            
  SET @trancount = @@trancount          
            
  IF @trancount = 0          
            BEGIN TRAN          
        else          
            SAVE TRAN @TransactionName          
          
  --Update nulls          
  UPDATE ExportAses.NetworkProvidersImport          
  SET           
   CarrierCode = LEFT(RTRIM(LTRIM(UPPER(CarrierCode))),2),          
   ProviderType = LEFT(RTRIM(LTRIM(UPPER(ProviderType))),20),          
   ReportDate = LEFT(RTRIM(LTRIM(UPPER(ReportDate))),8),          
   PMG = LEFT(RTRIM(LTRIM(UPPER(PMG))),10),          
   PMGName = LEFT(RTRIM(LTRIM(UPPER(PMGName))),80),          
   PMGFederalTaxId = LEFT(RTRIM(LTRIM(UPPER(PMGFederalTaxId))),20),            
   NPI = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(NPI), ''), NULL))),10),          
   FederalTaxId = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(FederalTaxId), ''), NULL))),9),          
   SpecialityCode = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(SpecialityCode), ''), NULL))),2),          
   AssignedLives = RTRIM(LTRIM(ISNULL(NULLIF(AssignedLives,''), 0))),          
   Name = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(Name), ''), NULL))),80),          
   LastName1 = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(LastName1), ''), NULL))),30),          
   LastName2 = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(LastName2), ''), NULL))),30),          
   FirstName = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(FirstName), ''), NULL))),50),          
   MiddleName = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(MiddleName), ''), NULL))),30),          
   AddressLine1 = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(AddressLine1), ''), NULL))),45),          
   AddressLine2 = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(AddressLine2), ''), NULL))),45),          
   City = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(City), ''), NULL))),45),          
   ZipCode = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(ZipCode), ''), NULL))),9),           
   Phone = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(Phone), ''), NULL))),20),          
   Fax = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(Fax), ''), NULL))),20),            
   LicenseNumber = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(LicenseNumber), ''), NULL))),10),           
   ContactPerson = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(ContactPerson), ''), NULL))),80),          
   Gender = LEFT(RTRIM(LTRIM(UPPER(Gender))),1),           
   Language = LEFT(RTRIM(LTRIM(UPPER(Language))),2),          
   ProviderCapacity = RTRIM(LTRIM(ISNULL(NULLIF(ProviderCapacity,''), 0))),          
   AcceptGender = RTRIM(LTRIM(ISNULL(NULLIF(AcceptGender,''), 0))),          
   AgeAcceptBegin = RTRIM(LTRIM(ISNULL(NULLIF(AgeAcceptBegin,''), 0))),          
   AgeAcceptEnd = RTRIM(LTRIM(ISNULL(NULLIF(AgeAcceptEnd,''), 0))),          
   ContractStatus = LEFT(RTRIM(LTRIM(UPPER(ContractStatus))),3),          
   MunicipalityCode = LEFT(RTRIM(LTRIM(UPPER(MunicipalityCode))),4),              
   NPIPmg = LEFT(RTRIM(LTRIM(ISNULL(NULLIF (UPPER(NPIPmg), ''), NULL))),10);          
           
  --Creando tabla temporal con formato del layout          
  If(OBJECT_ID('tempdb..#temp') Is Not Null)          
  Begin          
   Drop Table #Temp          
  End           
          
  create table #Temp          
  (        
   [Id] [int] NOT NULL,          
   [CarrierCode] [nvarchar](2)  NULL,          
   [ProviderType] [nvarchar](20)   NULL,          
   [ReportDate] [nvarchar](12)   NULL,          
   [PMG] [nvarchar](10)   NULL,          
   [PMGName] [nvarchar](80)   NULL,          
   [PMGFederalTaxId] [nvarchar](20)   NULL,          
   [NPI] [nvarchar](10)   NULL,          
   [FederalTaxId] [nvarchar](9)   NULL,          
   [SpecialityCode] [nvarchar](2)   NULL,          
   [AssignedLives] int NULL,          
   [Name] [nvarchar](80)   NULL,          
   [LastName1] [nvarchar](30)   NULL,          
   [LastName2] [nvarchar](30)   NULL,          
   [FirstName] [nvarchar](50)   NULL,          
   [MiddleName] [nvarchar](30)   NULL,          
   [AddressLine1] [nvarchar](45)   NULL,          
   [AddressLine2] [nvarchar](45)   NULL,          
   [City] [nvarchar](45) NULL,          
   [ZipCode] [nvarchar](9)    NULL,          
   [Phone] [nvarchar](20)   NULL,          
   [Fax] [nvarchar](20)   NULL,          
   [Sunday] [nvarchar](20)   NULL,          
   [Monday] [nvarchar](20)  NULL,          
   [Tuesday] [nvarchar](20)   NULL,          
   [Wednesday] [nvarchar](20)   NULL,          
   [Thursday] [nvarchar](20)   NULL,          
   [Friday] [nvarchar](20)   NULL,          
   [Saturday] [nvarchar](20)   NULL,          
   [LicenseNumber] [nvarchar](10)   NULL,          
   [ContactPerson] [nvarchar](80)  NULL,          
   [Gender] [nvarchar](2)   NULL,          
   [Language] [nvarchar](2)  NULL,          
   [ProviderCapacity] int NULL,          
   [AcceptGender] [nvarchar](2)   NULL,          
   [AgeAcceptBegin] int NULL,          
   [AgeAcceptEnd] int NULL,          
   [ContractStatus] [nvarchar](3)   NULL,          
   [MunicipalityCode] [nvarchar](4)  NULL,          
   [NPIPmg] [nvarchar](10)  NULL,          
   [ProcessHeaderId] [int] NULL,          
   [IsValidForImport] [bit] NULL,          
   [MessageInvalid] [nvarchar](max)   NULL          
  );          
           
  CREATE NONCLUSTERED INDEX [IX_PmgNPI]          
   ON #Temp([NPIPmg] ASC);          
  CREATE NONCLUSTERED INDEX [IX_PmgName_001]          
   ON #Temp([PMGName] ASC);          
  CREATE NONCLUSTERED INDEX [IX_PmgFederalTax_001]          
   ON #Temp([PMGFederalTaxId] ASC);          
  CREATE NONCLUSTERED INDEX [IX_PmgCode_001]          
   ON #Temp([PMG] ASC);          
  CREATE NONCLUSTERED INDEX [IX_PcpNPI_001]          
   ON #Temp([NPI] ASC);          
  CREATE NONCLUSTERED INDEX [IX_PcpFullName_001]          
   ON #Temp([Name] ASC);          
  CREATE NONCLUSTERED INDEX [IX_PcpFederalTaxId_001]          
   ON #Temp([FederalTaxId] ASC);          
  CREATE NONCLUSTERED INDEX [IX_Capacity_001]          
   ON #Temp([ProviderCapacity] ASC);          
  CREATE NONCLUSTERED INDEX [IX_AssignedLives_001]          
   ON #Temp([AssignedLives] ASC);          
          
          
  Insert Into #Temp          
  Select           
  [Id],          
  right(concat('00', [CarrierCode]),2),          
  [ProviderType],          
  [ReportDate],          
  [PMG],          
  [PMGName],          
  [PMGFederalTaxId],          
  [NPI],          
  [FederalTaxId],          
  right(concat('00', [SpecialityCode]),2),          
  [AssignedLives],          
  [Name],          
  [LastName1],          
  [LastName2],          
  [FirstName],          
  [MiddleName],          
  [AddressLine1],          
  [AddressLine2],          
  [City],          
  [ZipCode],          
  [Phone],          
  [Fax],          
  [Sunday],          
  [Monday],          
  [Tuesday],          
  [Wednesday],          
  [Thursday],          
  [Friday],          
  [Saturday],          
  [LicenseNumber],          
  [ContactPerson],          
  [Gender],          
  [Language],          
  [ProviderCapacity],          
  [AcceptGender],          
  [AgeAcceptBegin],          
  [AgeAcceptEnd],          
  [ContractStatus],          
  right(concat('0000', [MunicipalityCode]),4),          
  [NPIPmg],          
  [ProcessHeaderId],          
  [IsValidForImport],          
  [MessageInvalid]          
  from ExportAses.NetworkProvidersImport        
  WHERE ProcessHeaderId = @ProcessId          
        
		--select * from #Temp
		   
  --Extrayendo PMG          
  MERGE [Enrollment].[PrimaryMedicalGroup] AS target            
  USING (          
   select DISTINCT          
       PmgFederalTaxId,            
       PmgCode,           
       PmgName,           
       NPI          
     FROM (SELECT ROW_NUMBER() OVER (PARTITION BY PMGFederalTaxId ORDER BY NPI DESC) AS r,           
         CASE WHEN LTRIM(RTRIM(PMG))='0' THEN NULL WHEN LTRIM(RTRIM(PMG))='N/A' THEN NULL WHEN LTRIM(RTRIM(PMG))='' THEN NULL ELSE LTRIM(RTRIM(PMG)) END AS PmgCode,         
         CASE WHEN LTRIM(RTRIM(PMGName))='0' THEN NULL WHEN LTRIM(RTRIM(PMGName))='N/A' THEN NULL WHEN LTRIM(RTRIM(PMGName))='' THEN NULL ELSE LTRIM(RTRIM(PMGName)) END AS PmgName,         
         CASE WHEN LTRIM(RTRIM(PMGFederalTaxId))='0' THEN NULL WHEN LTRIM(RTRIM(PMGFederalTaxId))='N/A' THEN NULL WHEN LTRIM(RTRIM(PMGFederalTaxId))='' THEN NULL ELSE LTRIM(RTRIM(PMGFederalTaxId)) END AS PmgFederalTaxId,         
         CASE WHEN LTRIM(RTRIM(NPIPmg))='0' THEN NULL WHEN LTRIM(RTRIM(NPIPmg))='N/A' THEN NULL WHEN LTRIM(RTRIM(NPIPmg))='' THEN NULL ELSE LTRIM(RTRIM(NPIPmg)) END AS NPI  
      FROM #Temp          
     WHERE ProcessHeaderId = @ProcessId          
     AND IsValidForImport = 1          
     ) AS r          
    WHERE r.r = 1          
  )            
  AS source (PmgFederalTaxId, PmgCode, PmgName, NPI)          
  ON           
  (ISNULL(target.PmgFederalTaxId,'') = ISNULL(source.PmgFederalTaxId,'')     
   --AND ISNULL(target.PmgCode,'')  = ISNULL(source.PmgCode,'')        
   --AND ISNULL(target.PmgName,'') = ISNULL(source.PmgName,'')             
   --AND ISNULL(target.NPI,'') = ISNULL(source.NPI, '')    
   )          
  WHEN MATCHED THEN             
   UPDATE SET PmgCode = source.PmgCode,            
        PmgName = source.PmgName,            
        PmgFederalTaxId = source.PmgFederalTaxId,            
        NPI = source.NPI,          
        UpdatedBy = @UserName,          
        UpdatedOn = GETDATE()                  
  WHEN NOT MATCHED BY TARGET THEN           
  INSERT (PmgCode, PmgName, PmgFederalTaxId, NPI, CreatedBy, CreatedOn, [Enabled])          
  VALUES (PmgCode, PmgName, PmgFederalTaxId, NPI, @UserName, GETDATE(), 1);          
   
  update Enrollment.PrimaryMedicalGroup
	set
	PmgCode = NULL,
	PmgName = 'No Asignado',
	NPI = NULL
	where PmgFederalTaxId is null      
  --select * from #Temp        
        
  --Extrayendo Person           
  MERGE [Enrollment].[PersonPrimaryCarePhysician] AS target            
  USING (          
   SELECT DISTINCT          
   [FederalTaxId] COLLATE SQL_Latin1_General_CP1_CI_AS,          
   NPI COLLATE SQL_Latin1_General_CP1_CI_AS,          
   [FullName] COLLATE SQL_Latin1_General_CP1_CI_AS,          
   FirstName COLLATE SQL_Latin1_General_CP1_CI_AS,          
   MiddleName COLLATE SQL_Latin1_General_CP1_CI_AS,          
   [FirstLastName] COLLATE SQL_Latin1_General_CP1_CI_AS,          
   [SecondLastName] COLLATE SQL_Latin1_General_CP1_CI_AS,        
   COALESCE((SELECT Id FROM Enrollment.Genders WHERE Code = [Gender] COLLATE SQL_Latin1_General_CP1_CI_AS), 3) [GenderId]       
   --Enabled          
   FROM (SELECT ROW_NUMBER() OVER (PARTITION BY NPI ORDER BY NPI DESC) AS r,           
     CASE WHEN LTRIM(RTRIM(NPI))='0' THEN NULL WHEN LTRIM(RTRIM(NPI))='N/A' THEN NULL WHEN LTRIM(RTRIM(NPI))='' THEN NULL ELSE LTRIM(RTRIM(NPI)) END AS NPI,           
     CASE WHEN LTRIM(RTRIM(FederalTaxId))='0' THEN NULL WHEN LTRIM(RTRIM(FederalTaxId))='N/A' THEN NULL WHEN LTRIM(RTRIM(FederalTaxId))='' THEN NULL ELSE LTRIM(RTRIM(FederalTaxId)) END AS FederalTaxId,          
     CASE WHEN LTRIM(RTRIM(Name))='0' THEN NULL WHEN LTRIM(RTRIM(Name))='N/A' THEN NULL WHEN LTRIM(RTRIM(Name))='' THEN NULL ELSE LTRIM(RTRIM(Name)) END AS [FullName],          
     CASE WHEN LTRIM(RTRIM(FirstName))='0' THEN NULL WHEN LTRIM(RTRIM(FirstName))='N/A' THEN NULL WHEN LTRIM(RTRIM(FirstName))='' THEN NULL ELSE LTRIM(RTRIM(FirstName)) END AS FirstName,           
     CASE WHEN LTRIM(RTRIM(MiddleName))='0' THEN NULL WHEN LTRIM(RTRIM(MiddleName))='N/A' THEN NULL WHEN LTRIM(RTRIM(MiddleName))='' THEN NULL ELSE LTRIM(RTRIM(MiddleName)) END AS MiddleName,           
     CASE WHEN LTRIM(RTRIM(LastName1))='0' THEN NULL WHEN LTRIM(RTRIM(LastName1))='N/A' THEN NULL WHEN LTRIM(RTRIM(LastName1))='' THEN NULL ELSE LTRIM(RTRIM(LastName1)) END AS [FirstLastName],          
     CASE WHEN LTRIM(RTRIM(LastName2))='0' THEN NULL WHEN LTRIM(RTRIM(LastName2))='N/A' THEN NULL WHEN LTRIM(RTRIM(LastName2))='' THEN NULL ELSE LTRIM(RTRIM(LastName2)) END AS [SecondLastName],        
  CASE WHEN LTRIM(RTRIM(Gender))='0' THEN NULL WHEN LTRIM(RTRIM(Gender))='N/A' THEN NULL WHEN LTRIM(RTRIM(Gender))='' THEN NULL ELSE LTRIM(RTRIM(Gender)) END AS [Gender]        
     FROM #Temp          
     WHERE ProcessHeaderId = @ProcessId          
     AND IsValidForImport = 1          
      ) AS r          
   WHERE r.r = 1          
  )            
  AS source ([FederalTaxId], [NPI], [FullName], [FirstName], [MiddleName], [FirstLastName], [SecondLastName], [GenderId])          
  ON (ISNULL(source.[NPI],'') = ISNULL(target.[NPI],''))            
 WHEN NOT MATCHED BY TARGET THEN           
  INSERT ([FederalTaxId], [NPI], [FullName], [FirstName], [MiddleName], [FirstLastName], [SecondLastName], CreatedBy, CreatedOn, [Enabled], [GenderId])          
  VALUES ([FederalTaxId], [NPI], [FullName], [FirstName], [MiddleName], [FirstLastName], [SecondLastName], @UserName, GetDate(), 1, [GenderId]);               
  
	update Enrollment.PersonprimaryCarePhysician
		set 
		FederalTaxId = NULL,
		FullName = 'No Asignado',
		FirstName = NULL,
		MiddleName = NULL,
		FirstLastName = NULL,
		SecondLastName = NULL,
		GenderId = 3
		where NPI is NULL        

  --Extrayendo PCP          
  MERGE [Enrollment].[PrimaryCarePhysician] AS target            
  USING (          
   SELECT DISTINCT            
   coalesce((SELECT TOP (1) Id FROM Enrollment.PersonPrimaryCarePhysician AS PersonPcp WHERE (NPI = (CASE WHEN LTRIM(RTRIM(NPI))='0' THEN NULL WHEN LTRIM(RTRIM(Network.NPI))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.NPI))='' THEN NULL ELSE LTRIM(RTRIM(Network.NPI)) END) ) AND Enabled = 1), (select Id from Enrollment.PersonPrimaryCarePhysician where FullName = 'No Asignado')) AS PersonPrimaryCarePhysicianId, 
   (SELECT TOP (1) Id FROM Enrollment.Specialities AS Es WHERE (Network.SpecialityCode = Code) AND Enabled = 1) AS SpecialityId --,              
   --Capacity = ISNULL(MAX(ISNULL(NULLIF(Network.[ProviderCapacity],''),0)),9999) ,          
   --AmountOfLivesEnrolled = ISNULL(MAX(ISNULL(NULLIF(Network.[AssignedLives],''),0)),0)               
   FROM #Temp AS Network            
   WHERE ProcessHeaderId = @ProcessId          
   AND IsValidForImport = 1          
   GROUP BY NPI, SpecialityCode, MunicipalityCode, [AddressLine1], [AddressLine2],[City], [ZipCode]          
  )            
  AS source ([PersonPrimaryCarePhysicianId], [SpecialityId] ) --, Capacity, AmountOfLivesEnrolled      
  ON           
  (ISNULL(source.[PersonPrimaryCarePhysicianId],'')  =  ISNULL(target.[PersonPrimaryCarePhysicianId],'')           
   and ISNULL(source.[SpecialityId],'')  = ISNULL(target.[SpecialityId],'')      
   )              
   --WHEN MATCHED THEN             
   --UPDATE SET Capacity = source.Capacity --,            
        --AmountOfLivesEnrolled = source.AmountOfLivesEnrolled          
  WHEN NOT MATCHED BY TARGET THEN           
   INSERT ([PersonPrimaryCarePhysicianId], [SpecialityId], [CreatedBy],  [CreatedOn],  [Enabled]) --, Capacity, AmountOfLivesEnrolled      
   VALUES ([PersonPrimaryCarePhysicianId], [SpecialityId], @UserName,  GETDATE(),  1); --, Capacity, AmountOfLivesEnrolled       
              
  
  DELETE FROM [Enrollment].[PrimaryCarePhysicianDetail] --where PcpPmgMcoId in (select Id FROM [Enrollment].[PcpPmgMco] where McoId = (select McoId from Enrollment.ManagedCareOrganizations where CarrierCode = (select top 1 [CarrierCode] from #Temp)))  
  DELETE FROM [Enrollment].[PcpPmgMco] --where McoId = (select McoId from Enrollment.ManagedCareOrganizations where CarrierCode = (select top 1 [CarrierCode] from #Temp))  
  --delete from Enrollment.PrimaryCarePhysician where id not in (select distinct PCPId from Enrollment.Members)

  --Relacion PMG-PCP-MCO          
  MERGE [Enrollment].[PcpPmgMco] AS target            
  USING (          
   SELECT DISTINCT          
   T.McoId,          
   T.PmgId,          
   T.PcpId          
   FROM (          
    SELECT          
    (SELECT TOP (1) Id FROM Enrollment.ManagedCareOrganizations AS MCO WHERE (RTRIM(LTRIM(UPPER(ISNULL(CarrierCode,'')))) = ISNULL(Network.CarrierCode,'')) AND Enabled = 1) AS McoId,          
    coalesce((SELECT TOP (1) Id FROM Enrollment.PrimaryMedicalGroup AS Pmg WHERE (RTRIM(LTRIM(UPPER(ISNULL(PmgFederalTaxId,'')))) = ISNULL(Network.PMGFederalTaxId,'')) AND Enabled = 1), (SELECT TOP 1 Id FROM [Enrollment].[PrimaryMedicalGroup] where PmgName = 'No Asignado')) AS PmgId,          
    coalesce((SELECT TOP(1) Id FROM [Enrollment].[PrimaryCarePhysician]          
    WHERE PersonPrimaryCarePhysicianId = (SELECT TOP (1) Id FROM Enrollment.PersonPrimaryCarePhysician AS PersonPcp WHERE (ISNULL(RTRIM(LTRIM(UPPER(NPI))),'') = ISNULL(Network.NPI,'')) AND Enabled = 1)          
    AND SpecialityId = (SELECT TOP (1) ISNULL(Id,'') FROM Enrollment.Specialities AS Esp WHERE (ISNULL(Network.SpecialityCode,'') = ISNULL(RTRIM(LTRIM(UPPER(Code))),'')) AND Enabled = 1)       
    ), (SELECT TOP 1 pcp.Id FROM [Enrollment].[PrimaryCarePhysician] pcp INNER JOIN [Enrollment].[PersonPrimaryCarePhysician] ppcp ON pcp.PersonPrimaryCarePhysicianId= ppcp.Id where ppcp.FullName = 'No Asignado')) AS PcpId          
    FROM #Temp AS Network            
    WHERE ProcessHeaderId = @ProcessId          
     AND IsValidForImport = 1           
   ) AS T          
  )            
  AS source (McoId, PmgId, PcpId)          
  ON           
  (ISNULL(source.McoId,'')  =  ISNULL(target.McoId,'')           
   and ISNULL(source.PmgId,'')  = ISNULL(target.PmgId,'')          
   and ISNULL(source.PcpId,'')  = ISNULL(target.[PrimaryCarePhysicianId],'')    
   )                
  WHEN NOT MATCHED BY TARGET THEN           
   INSERT (McoId, PmgId, [PrimaryCarePhysicianId], [CreatedBy],  [CreatedOn],  [Enabled])          
   VALUES (McoId, PmgId, PcpId, @UserName,  GETDATE(),  1);           
         
         
   --Extrayendo Detail PCP    
   INSERT INTO [Enrollment].[PrimaryCarePhysicianDetail](Phone, CreatedBy, CreatedOn, [Enabled], [AddressLineOne], [AddressLineTwo],[City], [ZipCode], [State], PcpPmgMcoId, MunicipalityId, AmountOfLivesEnrolled, Capacity)      
   SELECT DISTINCT      
    A.Phone,      
    @UserName,      
    GETDATE(),      
    1,      
    A.AddressLine1,      
    A.AddressLine2,      
    A.City,      
    A.ZipCode,      
    'PR',      
    (SELECT TOP 1 PPM.Id FROM Enrollment.PcpPmgMco PPM WHERE PPM.PrimaryCarePhysicianId= A.PcpId AND PPM.McoId = A.McoId AND PPM.PmgId = A.PmgId),      
    A.MunicipalityId,      
    A.AssignedLives,      
    A.ProviderCapacity      
   FROM (      
   SELECT       
    CASE WHEN LTRIM(RTRIM(Network.PMGFederalTaxId))='0' THEN NULL WHEN LTRIM(RTRIM(Network.PMGFederalTaxId))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.PMGFederalTaxId))='' THEN NULL ELSE LTRIM(RTRIM(Network.PMGFederalTaxId)) END PMGFederalTaxId      
    ,CASE WHEN LTRIM(RTRIM(Network.CarrierCode))='0' THEN NULL WHEN LTRIM(RTRIM(Network.CarrierCode))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.CarrierCode))='' THEN NULL ELSE LTRIM(RTRIM(Network.CarrierCode)) END CarrierCode       
    ,CASE WHEN LTRIM(RTRIM(Network.NPI))='0' THEN NULL WHEN LTRIM(RTRIM(Network.NPI))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.NPI))='' THEN NULL ELSE LTRIM(RTRIM(Network.NPI)) END NPI       
    ,CASE WHEN LTRIM(RTRIM(Network.SpecialityCode))='0' THEN NULL WHEN LTRIM(RTRIM(Network.SpecialityCode))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.SpecialityCode))='' THEN NULL ELSE LTRIM(RTRIM(Network.SpecialityCode)) END SpecialityCode       
    , coalesce((SELECT TOP 1 pcp.Id FROM Enrollment.PrimaryCarePhysician pcp       
      INNER JOIN Enrollment.PersonPrimaryCarePhysician person ON person.Id = pcp.PersonPrimaryCarePhysicianId       
      INNER JOIN Enrollment.Specialities spe ON spe.Id = pcp.SpecialityId       
      WHERE person.NPI = Network.NPI AND ISNULL(Network.SpecialityCode,'') = ISNULL(RTRIM(LTRIM(UPPER(spe.Code))),'')), (SELECT TOP 1 pcp.Id FROM [Enrollment].[PrimaryCarePhysician] pcp INNER JOIN [Enrollment].[PersonPrimaryCarePhysician] ppcp ON pcp.PersonPrimaryCarePhysicianId= ppcp.Id where FullName = 'No Asignado')) PcpId      
    , (SELECT TOP 1 Mco.Id FROM Enrollment.ManagedCareOrganizations Mco WHERE Mco.CarrierCode = Network.CarrierCode) McoId      
    , coalesce((SELECT TOP 1 Pmg.Id FROM Enrollment.PrimaryMedicalGroup Pmg WHERE (RTRIM(LTRIM(UPPER(ISNULL(Pmg.PmgFederalTaxId,'')))) = ISNULL(Network.PMGFederalTaxId,''))), (SELECT TOP 1 Id FROM [Enrollment].[PrimaryMedicalGroup] where PmgName = 'No Asignado')) PmgId      
    , CASE WHEN LTRIM(RTRIM(Network.Phone))='0' THEN NULL WHEN LTRIM(RTRIM(Network.Phone))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.Phone))='' THEN NULL ELSE LTRIM(RTRIM(Network.Phone)) END Phone      
    , CASE WHEN LTRIM(RTRIM(Network.AddressLine1))='0' THEN NULL WHEN LTRIM(RTRIM(Network.AddressLine1))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.AddressLine1))='' THEN NULL ELSE LTRIM(RTRIM(Network.AddressLine1)) END AddressLine1      
    , CASE WHEN LTRIM(RTRIM(Network.AddressLine2))='0' THEN NULL WHEN LTRIM(RTRIM(Network.AddressLine2))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.AddressLine2))='' THEN NULL ELSE LTRIM(RTRIM(Network.AddressLine2)) END AddressLine2      
    , CASE WHEN LTRIM(RTRIM(Network.City))='0' THEN NULL WHEN LTRIM(RTRIM(Network.City))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.City))='' THEN NULL ELSE LTRIM(RTRIM(Network.City)) END City      
    , CASE WHEN LTRIM(RTRIM(Network.ZipCode))='0' THEN NULL WHEN LTRIM(RTRIM(Network.ZipCode))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.ZipCode))='' THEN NULL ELSE LTRIM(RTRIM(Network.ZipCode)) END ZipCode      
    , CASE WHEN LTRIM(RTRIM(Network.MunicipalityCode))='0' THEN NULL WHEN LTRIM(RTRIM(Network.MunicipalityCode))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.MunicipalityCode))='' THEN NULL ELSE LTRIM(RTRIM(Network.MunicipalityCode)) END MunicipalityCode    
  
    , (SELECT Id FROM Enrollment.Municipalities WHERE Code = Network.MunicipalityCode) MunicipalityId   
    , CASE WHEN LTRIM(RTRIM(Network.ProviderCapacity))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.ProviderCapacity))='' THEN NULL ELSE LTRIM(RTRIM(Network.ProviderCapacity)) END ProviderCapacity      
    , CASE WHEN LTRIM(RTRIM(Network.AssignedLives))='N/A' THEN NULL WHEN LTRIM(RTRIM(Network.AssignedLives))='' THEN NULL ELSE LTRIM(RTRIM(Network.AssignedLives)) END AssignedLives      
    FROM #Temp AS Network      
    WHERE ProcessHeaderId = @ProcessId          
      AND IsValidForImport = 1       
    )A       
           
  UPDATE Enrollment.PersonPrimaryCarePhysician
  SET FullName = CONCAT(COALESCE(LTRIM(RTRIM(FirstName)), ''), ' ', COALESCE(LTRIM(RTRIM(MiddleName)), ''), ' ', COALESCE(LTRIM(RTRIM(FirstLastName)), ''), ' ', COALESCE(LTRIM(RTRIM(SecondLastName)), '') )
  WHERE FullName IS NULL

  SET @Error = @@ERROR            
            
  if @trancount = 0           
   commit;          
          
  SELECT          
   NULL AS ProcessHeaderId          
  ,@ProcessType AS ProcessType          
  ,@Error AS ErrorNumber          
  ,@Procedure AS ProcedureName          
  ,NULL AS ErrorLine          
  ,NULL AS ProcessMessage          
 END TRY            
 BEGIN CATCH          
     DECLARE @xstate int;          
        select @xstate = XACT_STATE();          
  SELECT          
   NULL AS ProcessHeaderId          
  ,@ProcessType AS ProcessType          
  ,CAST(ERROR_NUMBER() as varchar(10)) AS ErrorNumber          
  ,ERROR_PROCEDURE() AS ProcedureName          
  ,CAST(ERROR_LINE() as varchar(10)) AS ErrorLine          
  ,ERROR_MESSAGE() AS ProcessMessage          
          
  if @xstate = -1          
            rollback;          
        if @xstate = 1 and @trancount = 0          
            rollback          
        if @xstate = 1 and @trancount > 0          
            rollback transaction @TransactionName;          
          
            
 END CATCH          
END;