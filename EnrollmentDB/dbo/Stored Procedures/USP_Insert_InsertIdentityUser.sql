CREATE proc USP_Insert_InsertIdentityUser
AS
BEGIN
 DECLARE @SqlStatement VARCHAR(500)    
 DECLARE @IdInserted INT    
 DECLARE @Procedure VARCHAR(100)    
 DECLARE @Rows int    
 DECLARE @Error VARCHAR(300)    
 DECLARE @Start DATETIME    
 DECLARE @End DATETIME  
 DECLARE @INDICE INT=1
 DECLARE @CANTIDAD INT
 DECLARE @TransactionName VARCHAR(50) = 'PROCESS_INSERT_USERS'

 SET @Procedure = OBJECT_NAME(@@PROCID)    
SET @Start = GETDATE()  
  
SELECT @CANTIDAD=COUNT(1) FROM TMP_MPIShort

 BEGIN TRY    



BEGIN TRAN @TransactionName   
declare @userprocess varchar(200)
  select @userprocess='CargaUsuario' + convert(varchar, getdate(), 112)

  insert into [Identity].IdentityUsers (
     UserName
    ,FirstName
    ,LastName1
    ,LastName2
    ,SSNLast4
    ,ZipCode
    ,DateOfBirth
    ,IsAdministrator
    ,Email
    ,EmailConfirmed
    ,PasswordHash
    ,SecurityStamp
    ,PhoneNumber
    ,PhoneNumberConfirmed
    ,TwoFactorEnabled
    ,LockoutEndDateUtc
    ,LockoutEnabled
    ,AccessFailedCount
    ,CreatedBy
    ,CreatedOn
    ,UpdatedBy
    ,UpdatedOn
    ,Enabled
    ,MemberId
    ,Roles
    ,OptIn
    ,PhoneNumber2
    ,Email2
    ,MPI
    ,HasDefaultCredentials
  ) 
  SELECT distinct
    ltrim(rtrim(t.email))  AS UserName                -- UserName - varchar(500)
    ,ltrim(rtrim(m.firstname))
    ,ltrim(rtrim(m.firstlastname))
    ,ltrim(rtrim(m.secondlastname))
    ,right(m.ssn,4)
    ,ltrim(rtrim(Fam.ResidenceAddressZip4))
    ,ltrim(rtrim(m.DateOfBirth))
    ,NULL AS IsAdministrator        -- IsAdministrator - bit
    ,ltrim(rtrim(t.email))  AS Email                  -- Email - varchar(500)
    ,0   AS EmailConfirmed          -- EmailConfirmed - bit
    ,'AFrn1sZlkb8z5+V05wMv2a08uxlwvkknNVhyL8dfbdueJQQkIpTe00bBBT6pHbBW8A=='
    ,'b0ea6e64-a468-425b-868b-45a3e745bbff'
    ,ltrim(rtrim(Fam.Phone))
    ,0   AS PhoneNumberConfirmed    -- PhoneNumberConfirmed - bit
    ,0   AS TwoFactorEnabled        -- TwoFactorEnabled - bit
    ,getdate()   AS LockoutEndDateUtc      -- LockoutEndDateUtc - datetime
    ,0   AS LockoutEnabled          -- LockoutEnabled - bit
    ,0   AS AccessFailedCount       -- AccessFailedCount - int
    ,@userprocess
    ,getdate()
    ,null
    ,NULL
    ,1
    ,ltrim(rtrim(m.Id))
    ,NULL AS Roles                  -- Roles - varchar(10)
    ,1 AS OptIn                  -- OptIn - bit
    ,NULL AS PhoneNumber2           -- PhoneNumber2 - varchar(100)
    ,NULL AS Email2                 -- Email2 - varchar(500)
    ,ltrim(rtrim(m.MPI))
    ,0   AS HasDefaultCredentials   -- HasDefaultCredentials - bit
  FROM Enrollment.Members m with(nolock)
  inner join Enrollment.Families Fam with(nolock) ON Fam.Id = m.FamilyId
  inner join TMP_MPIShort t (nolock) on t.MPI=m.MPIshort
  where not exists(select * from [Identity].[IdentityUsers] where MemberId=m.Id) and
    t.email<>''

  insert into [Identity].IdentityRolUsers (
     ApplicationId
    ,UserId
    ,RoleId
    ,CreatedBy
    ,CreatedOn
    ,UpdatedBy
    ,UpdatedOn
    ,Enabled
  ) SELECT 
     1
    ,iu.Id
    ,3
    ,'CargaUsuario' + convert(varchar, getdate(), 112)
    ,getdate()
    ,null
    ,null
    ,1
  FROM [Identity].IdentityUsers iu with(NOLOCK) 
  where iu.CreatedBy=@userprocess
  
    insert into [Identity].IdentityRolUsers (
     ApplicationId
    ,UserId
    ,RoleId
    ,CreatedBy
    ,CreatedOn
    ,UpdatedBy
    ,UpdatedOn
    ,Enabled
  ) SELECT 
     1
    ,iu.Id
    ,1
    ,'CargaUsuario' + convert(varchar, getdate(), 112)
    ,getdate()
    ,null
    ,null
    ,1
  FROM [Identity].IdentityUsers iu with(NOLOCK) 
  where iu.CreatedBy=@userprocess
  

SET @Rows = @@ROWCOUNT    
SET @Error = @@ERROR    

COMMIT TRAN @TransactionName    
 END TRY    
 BEGIN CATCH    
  ROLLBACK TRAN @TransactionName    
  SELECT    
   NULL As ProcessHeaderId       
  ,CAST(ERROR_NUMBER() as varchar(10)) AS ErrorNumber    
  ,ERROR_PROCEDURE() AS ProcedureName    
  ,CAST(ERROR_LINE() as varchar(10)) AS ErrorLine    
  ,ERROR_MESSAGE() AS ProcessMessage   
   set @INDICE=@INDICE+1
 END CATCH   
 set @INDICE=@INDICE+1
 END