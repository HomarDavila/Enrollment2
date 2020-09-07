--declare
--@PROCESS_HEADER_ID INT=140,  
--@DIAS INT=1
CREATE PROC [ExportAses].[USP_AutomaticCloseIfNotRejected](  
@PROCESS_HEADER_ID INT,  
@DIAS INT)   
AS  
BEGIN  
  
 DECLARE @Error VARCHAR(300)    
 DECLARE @Procedure VARCHAR(100)  
  
 SET @Procedure = OBJECT_NAME(@@PROCID)    
  
 BEGIN TRAN UPDATES; --DECLARANDO TRANSACCION  
   
  BEGIN TRY --ABRIENDO TRY  
   UPDATE  ER SET ER.StatusId=6
   FROM [Enrollment].[EnrollmentHistories] ER (NOLOCK)  
   INNER JOIN ExportAses.ExportFileForAses EF (NOLOCK) ON ER.Id=EF.MEMBER_ID_ENROLLMENTHISTORY  
   where ER.StatusId=2   
   AND FORMAT(DATEADD(DAY, @DIAS, ER.CreatedOn), 'MMddyyyy') = FORMAT(GETDATE(), 'MMddyyyy') AND  EF.ODSI_FAMILY_ID 
   NOT IN (SELECT RI.Familiy_ID FROM ExportAses.ImportRejectfileFromAses  RI  
   WHERE RI.PROCESS_HEADERID = @PROCESS_HEADER_ID)
    
   UPDATE  EF SET EF.StatusId=6
   FROM [Enrollment].[EnrollmentHistories] ER (NOLOCK)  
   INNER JOIN ExportAses.ExportFileForAses EF (NOLOCK) ON ER.Id=EF.MEMBER_ID_ENROLLMENTHISTORY  
   where ER.StatusId=2   
   AND FORMAT(DATEADD(DAY, @DIAS, ER.CreatedOn), 'MMddyyyy') = FORMAT(GETDATE(), 'MMddyyyy') AND  EF.ODSI_FAMILY_ID 
   NOT IN (SELECT RI.Familiy_ID FROM ExportAses.ImportRejectfileFromAses  RI  
   WHERE RI.PROCESS_HEADERID = @PROCESS_HEADER_ID)

   COMMIT TRAN UPDATES; --SI TODO ES CORRECTO SE PROCEDERA A ACTUALIZAR LA INFORMACION  
  
   SELECT      
   '0' AS ProcessType      
   ,@Error AS ErrorNumber      
   ,@Procedure AS ProcedureName      
   ,NULL AS ErrorLine      
   ,NULL AS ProcessMessage      
  
  END TRY  
  BEGIN CATCH -- SI EXISTE UN ERROR  
  SET @Error = @@ERROR     
   IF @@TRANCOUNT>0  
    BEGIN  
     ROLLBACK TRAN UPDATES -- SE ELIMINARA LA TRANSACCION Y NO SE ACTUALIZARA LOS DATOS  
    END  
   SELECT      
   '-1' AS ProcessType      
   ,CAST(ERROR_NUMBER() AS VARCHAR) AS ErrorNumber      
   ,ERROR_PROCEDURE() AS ProcedureName      
   ,CAST(ERROR_LINE() AS VARCHAR) AS ErrorLine      
   ,ERROR_MESSAGE() AS ProcessMessage      
  
  END CATCH  
END