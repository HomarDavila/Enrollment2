--declare
--@PROCESS_HEADER_ID INT=135,
--@DIAS INT=1
CREATE PROC [ExportAses].[USP_UpdateStatusWhenRejected]
@PROCESS_HEADER_ID INT,
@DIAS INT
AS
BEGIN

 DECLARE @Error VARCHAR(300)  
 DECLARE @Procedure VARCHAR(100)
 DECLARE @Rows int           

 SET @Procedure = OBJECT_NAME(@@PROCID)  

	BEGIN TRAN UPDATES; --DECLARANDO TRANSACCION
	
		BEGIN TRY --ABRIENDO TRY
			UPDATE  ER SET ER.StatusId=3
			FROM [Enrollment].[EnrollmentHistories] ER (NOLOCK)
			INNER JOIN ExportAses.ImportRejectfileFromAses IR (NOLOCK) ON IR.FAMILIY_ID=ER.MPIshort
			INNER JOIN ExportAses.ExportFileForAses EF (NOLOCK) ON ER.Id=EF.MEMBER_ID_ENROLLMENTHISTORY
			where ER.StatusId=2 AND IR.PROCESS_HEADERID=@PROCESS_HEADER_ID AND DATEDIFF(DAY,ER.CreatedOn,GETDATE())<=@DIAS
			   
			UPDATE  EF SET EF.StatusId=3
			FROM [Enrollment].[EnrollmentHistories] ER (NOLOCK)
			INNER JOIN ExportAses.ImportRejectfileFromAses IR (NOLOCK) ON IR.FAMILIY_ID=ER.MPIshort
			INNER JOIN ExportAses.ExportFileForAses EF (NOLOCK) ON ER.Id=EF.MEMBER_ID_ENROLLMENTHISTORY
			where ER.StatusId=2 AND IR.PROCESS_HEADERID=@PROCESS_HEADER_ID AND DATEDIFF(DAY,ER.CreatedOn,GETDATE())<=@DIAS

			SET @Rows = @@ROWCOUNT      
			SET @Error = @@ERROR 

			COMMIT TRAN UPDATES; --SI TODO ES CORRECTO SE PROCEDERA A ACTUALIZAR LA INFORMACION
			
			SELECT    
			'0' AS ProcessType    
			,@Error AS ErrorNumber    
			,@Procedure AS ProcedureName    
			,NULL AS ErrorLine    
			,CAST(@Rows as varchar(10)) + ' Affected Rows' AS ProcessMessage    

			EXEC [ExportAses].[USP_AutomaticCloseIfNotRejected] @PROCESS_HEADER_ID,@DIAS

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

--(fechacreacion el histories + dias)