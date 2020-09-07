
CREATE PROCEDURE [ExportAses].[ExecuteNormalizationNetworkProviders_PRD_DLM]     
 @ProcessId int,   
 @ProcessType VARCHAR(20),  
 @UserName VARCHAR(30)  
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
   PMG = LEFT(RTRIM(LTRIM(UPPER(PMG))),4),  
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
   [PMG] [nvarchar](4)   NULL,  
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
  [CarrierCode],  
  [ProviderType],  
  [ReportDate],  
  [PMG],  
  [PMGName],  
  [PMGFederalTaxId],  
  [NPI],  
  [FederalTaxId],  
  [SpecialityCode],  
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
  [MunicipalityCode],  
  [NPIPmg],  
  [ProcessHeaderId],  
  [IsValidForImport],  
  [MessageInvalid]  
  from ExportAses.NetworkProvidersImport  
  WHERE ProcessHeaderId = @ProcessId  
   
   
  --Extrayendo PMG  
  MERGE [Enrollment].[PrimaryMedicalGroup] AS target    
  USING (  
   select DISTINCT   
       PmgCode,   
       PmgName,   
       PmgFederalTaxId,   
       NPI  
     FROM (SELECT ROW_NUMBER() OVER (PARTITION BY PMGFederalTaxId ORDER BY NPI DESC) AS r,   
         PMG AS PmgCode,   
         PMGName AS PmgName,   
         PMGFederalTaxId AS PmgFederalTaxId,   
         NPIPmg AS NPI          
      FROM #Temp  
     WHERE ProcessHeaderId = @ProcessId  
     AND IsValidForImport = 1  
     ) AS r  
    WHERE r.r = 1  
  )    
  AS source (PmgCode, PmgName, PmgFederalTaxId, NPI)  
  ON   
  (ISNULL(target.PmgCode,'')  = ISNULL(source.PmgCode,'')   
   AND ISNULL(target.PmgName,'') = ISNULL(source.PmgName,'')  
   AND ISNULL(target.PmgFederalTaxId,'') = ISNULL(source.PmgFederalTaxId,'')  
   AND ISNULL(target.NPI,'') = ISNULL(source.NPI, ''))  
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
  
    --Extrayendo Person   
  MERGE [Enrollment].[PersonPrimaryCarePhysician] AS target    
  USING (  
   SELECT DISTINCT  
   [FederalTaxId],  
   NPI,  
   [FullName],  
   FirstName,  
   MiddleName,  
   [FirstLastName],  
   [SecondLastName]  
   Enabled  
   FROM (SELECT ROW_NUMBER() OVER (PARTITION BY NPI ORDER BY NPI DESC) AS r,   
     NPI AS NPI,   
     FederalTaxId AS FederalTaxId,  
     Name [FullName],  
     FirstName FirstName,   
     MiddleName MiddleName,   
     LastName1 [FirstLastName],  
     LastName2 [SecondLastName]      
     FROM #Temp  
     WHERE ProcessHeaderId = @ProcessId  
     AND IsValidForImport = 1  
      ) AS r  
   WHERE r.r = 1  
  )    
  AS source ([FederalTaxId], [NPI], [FullName], [FirstName], [MiddleName], [FirstLastName], [SecondLastName])  
  ON (ISNULL(source.[NPI],'') = ISNULL(target.[NPI],''))    
 WHEN NOT MATCHED BY TARGET THEN   
  INSERT ([FederalTaxId], [NPI], [FullName], [FirstName], [MiddleName], [FirstLastName], [SecondLastName], CreatedBy, CreatedOn, [Enabled])  
  VALUES ([FederalTaxId], [NPI], [FullName], [FirstName], [MiddleName], [FirstLastName], [SecondLastName], @UserName, GetDate(), 1);  
  
  
  --Extrayendo PCP  
  MERGE [Enrollment].[PrimaryCarePhysician] AS target    
  USING (  
   SELECT DISTINCT    
   (SELECT TOP (1) Id FROM Enrollment.PersonPrimaryCarePhysician AS PersonPcp WHERE (RTRIM(LTRIM(UPPER(ISNULL(NPI,'')))) = ISNULL(Network.NPI,'')) AND Enabled = 1) AS PersonPrimaryCarePhysicianId,     
   (SELECT TOP (1) Id FROM Enrollment.Specialities AS Es WHERE (Network.SpecialityCode = Code) AND Enabled = 1) AS SpecialityId,   
   (SELECT TOP (1) Id FROM Enrollment.Municipalities AS Muni WHERE (Network.MunicipalityCode = Code) AND Enabled = 1) AS MunicipalityId,    
   [AddressLine1],  
   [AddressLine2],  
   [City],  
   [ZipCode],  
   Capacity = ISNULL(MAX(ISNULL(NULLIF(Network.[ProviderCapacity],''),0)),9999) ,  
   AmountOfLivesEnrolled = ISNULL(MAX(ISNULL(NULLIF(Network.[AssignedLives],''),0)),0)       
   FROM #Temp AS Network    
   WHERE ProcessHeaderId = @ProcessId  
   AND IsValidForImport = 1  
   GROUP BY NPI, SpecialityCode, MunicipalityCode, [AddressLine1], [AddressLine2],[City], [ZipCode]  
  )    
  AS source ([PersonPrimaryCarePhysicianId], [SpecialityId], [MunicipalityId], [AddressLine1], [AddressLine2],[City], [ZipCode] , Capacity, AmountOfLivesEnrolled )  
  ON   
  (ISNULL(source.[PersonPrimaryCarePhysicianId],'')  =  ISNULL(target.[PersonPrimaryCarePhysicianId],'')   
   and ISNULL(source.[SpecialityId],'')  = ISNULL(target.[SpecialityId],'')  
   and ISNULL(source.[MunicipalityId],'')  = ISNULL(target.[MunicipalityId],'')   
   and ISNULL(source.[AddressLine1],'')  =  ISNULL(target.[AddressLineOne],'')  
   and ISNULL(source.[AddressLine2],'')  =  ISNULL(target.[AddressLineTwo],'')  
   and ISNULL(source.[City],'')  =  ISNULL(target.[City],'')  
   and ISNULL(source.[ZipCode],'')  =  ISNULL(target.[ZipCode],''))      
   WHEN MATCHED THEN     
   UPDATE SET Capacity = source.Capacity,    
        AmountOfLivesEnrolled = source.AmountOfLivesEnrolled  
  WHEN NOT MATCHED BY TARGET THEN   
   INSERT ([PersonPrimaryCarePhysicianId], [SpecialityId], [MunicipalityId], [AddressLineOne], [AddressLineTwo],[City], [ZipCode], [State], Capacity, AmountOfLivesEnrolled, [CreatedBy],  [CreatedOn],  [Enabled])  
   VALUES ([PersonPrimaryCarePhysicianId], [SpecialityId], [MunicipalityId], [AddressLine1], [AddressLine2], [City], [ZipCode], 'PR', Capacity, AmountOfLivesEnrolled , @UserName,  GETDATE(),  1);   
 

  
  --Extrayendo Detail PCP  
  MERGE [Enrollment].[PrimaryCarePhysicianDetail] AS target    
  USING (  
   SELECT DISTINCT  
   (SELECT TOP(1) Id FROM [Enrollment].[PrimaryCarePhysician]  
   WHERE PersonPrimaryCarePhysicianId = (SELECT TOP (1) Id FROM Enrollment.PersonPrimaryCarePhysician AS PersonPcp WHERE (ISNULL(RTRIM(LTRIM(UPPER(NPI))),'') = ISNULL(Network.NPI,'')) AND Enabled = 1)  
   AND SpecialityId = (SELECT TOP (1) ISNULL(Id,'') FROM Enrollment.Specialities AS Esp WHERE (ISNULL(Network.SpecialityCode,'') = ISNULL(RTRIM(LTRIM(UPPER(Code))),'') AND Enabled = 1))   
   AND MunicipalityId = (SELECT TOP (1) ISNULL(Id,'') FROM Enrollment.Municipalities AS Muni WHERE (ISNULL(Network.MunicipalityCode,'') = ISNULL(RTRIM(LTRIM(UPPER(Code))),'') AND Enabled = 1))  
   AND ISNULL([AddressLineOne],'') = ISNULL(Network.[AddressLine1],'')  
   AND ISNULL([AddressLine2],'') =  ISNULL(Network.[AddressLine2],'')  
   AND ISNULL([City],'') = ISNULL(Network.[City],'')  
   AND ISNULL([ZipCode],'') = ISNULL(Network.[ZipCode],'')  
   ) AS PrimaryCarePhysicianId,  
   Phone AS Phone    
   FROM #Temp AS Network  
   WHERE ProcessHeaderId = @ProcessId  
      AND IsValidForImport = 1  
  )    
  AS source (PrimaryCarePhysicianId, Phone)  
  ON   
  (ISNULL(source.PrimaryCarePhysicianId,'') = ISNULL(target.PrimaryCarePhysicianId,'')  
   AND ISNULL(source.Phone,'') = ISNULL(target.Phone,'')   
  )       
 WHEN NOT MATCHED BY TARGET THEN   
  INSERT (PrimaryCarePhysicianId, Phone, CreatedBy, CreatedOn, [Enabled])  
  VALUES (PrimaryCarePhysicianId, Phone, @UserName, GetDate(), 1);  

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
    (SELECT TOP (1) Id FROM Enrollment.PrimaryMedicalGroup AS Pmg WHERE (RTRIM(LTRIM(UPPER(ISNULL(PmgFederalTaxId,'')))) = ISNULL(Network.PMGFederalTaxId,'')) AND Enabled = 1) AS PmgId,  
    (SELECT TOP(1) Id FROM [Enrollment].[PrimaryCarePhysician]  
    WHERE PersonPrimaryCarePhysicianId = (SELECT TOP (1) Id FROM Enrollment.PersonPrimaryCarePhysician AS PersonPcp WHERE (ISNULL(RTRIM(LTRIM(UPPER(NPI))),'') = ISNULL(Network.NPI,'')) AND Enabled = 1)  
    AND SpecialityId = (SELECT TOP (1) ISNULL(Id,'') FROM Enrollment.Specialities AS Esp WHERE (ISNULL(Network.SpecialityCode,'') = ISNULL(RTRIM(LTRIM(UPPER(Code))),'')) AND Enabled = 1)   
    AND MunicipalityId = (SELECT TOP (1) ISNULL(Id,'') FROM Enrollment.Municipalities AS Muni WHERE (ISNULL(Network.MunicipalityCode,'') = ISNULL(RTRIM(LTRIM(UPPER(Code))),'')) AND Enabled = 1)   
    AND ISNULL([AddressLineOne],'') = ISNULL(Network.[AddressLine1],'')  
    AND ISNULL([AddressLine2],'') =  ISNULL(Network.[AddressLine2],'')  
    AND ISNULL([City],'') = ISNULL(Network.[City],'')  
    AND ISNULL([ZipCode],'') = ISNULL(Network.[ZipCode],'')  
    ) AS PcpId  
    FROM #Temp AS Network    
    WHERE ProcessHeaderId = @ProcessId  
     AND IsValidForImport = 1   
   ) AS T  
  )    
  AS source (McoId, PmgId, PcpId)  
  ON   
  (ISNULL(source.McoId,'')  =  ISNULL(target.McoId,'')   
   and ISNULL(source.PmgId,'')  = ISNULL(target.PmgId,'')  
   and ISNULL(source.PcpId,'')  = ISNULL(target.[PrimaryCarePhysicianId],''))        
  WHEN NOT MATCHED BY TARGET THEN   
   INSERT (McoId, PmgId, [PrimaryCarePhysicianId], [CreatedBy],  [CreatedOn],  [Enabled])  
   VALUES (McoId, PmgId, PcpId, @UserName,  GETDATE(),  1);   
   
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