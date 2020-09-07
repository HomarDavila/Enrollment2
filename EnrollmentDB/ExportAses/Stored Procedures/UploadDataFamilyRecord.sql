﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [ExportAses].[UploadDataFamilyRecord] 
	@ExpFilePath VARCHAR(500),
	@Exp_FileName VARCHAR(100),
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
	CREATE TABLE #TEMP (Data VARCHAR(2000) COLLATE SQL_Latin1_General_CP1_CI_AI)
	BEGIN TRY
		SET @Procedure = OBJECT_NAME(@@PROCID)
		SET @Start = GETDATE()
		SET @SqlStatement = 'BULK INSERT #TEMP FROM ''' + @ExpFilePath + ''' WITH(ROWTERMINATOR = ''' + CHAR(10) + ''', CODEPAGE = ''' + 'ACP' + ''', DATAFILETYPE = ''' + 'widechar' + ''')'
		EXECUTE (@SqlStatement)

		SELECT 
			SUBSTRING(Data,1,1) RECORD_TYPE,
			SUBSTRING(Data,2,1) TRAN_ID,
			SUBSTRING(Data,3,8) PROCESS_DATE,
			SUBSTRING(Data,11,9) FAMILY_SSN,
			SUBSTRING(Data,20,2) FAMILY_SUFFIX,
			SUBSTRING(Data,22,14) FILLER_,
			SUBSTRING(Data,36,11) FAMILY_ID,
			SUBSTRING(Data,47,15) CONTACT_LAST_NAME_1,
			SUBSTRING(Data,62,15) CONTACT_LAST_NAME_2,
			SUBSTRING(Data,77,20) CONTACT_FIRST_NAME,
			SUBSTRING(Data,97,1) REGION,
			SUBSTRING(Data,98,4) MUNICIPALITY,
			SUBSTRING(Data,102,4) FACILITY,
			SUBSTRING(Data,106,1) INVESTIGATION_IND,
			SUBSTRING(Data,107,1) TRANSACTION_TYPE,
			SUBSTRING(Data,108,8) EFFECTIVE_DATE,
			SUBSTRING(Data,116,1) FINANCIAL_RESP_PCT,
			SUBSTRING(Data,117,2) CERTIFIER_NUMBER,
			SUBSTRING(Data,119,8) EXPIRATION_DATE,
			SUBSTRING(Data,127,1) COND_ELIG_IND,
			SUBSTRING(Data,128,25) MAILING_ADDRESS1,
			SUBSTRING(Data,153,25) MAILING_ADDRESS2,
			SUBSTRING(Data,178,16) MAILING_CITY,
			SUBSTRING(Data,194,5) MAILING_ZIP,
			SUBSTRING(Data,199,4) MAILING_ZIP4,
			SUBSTRING(Data,203,25) RESIDENCE_ADDRESS1,
			SUBSTRING(Data,228,25) RESIDENCE_ADDRESS2,
			SUBSTRING(Data,253,16) RESIDENCE_CITY,
			SUBSTRING(Data,269,5) RESIDENCE_ZIP,
			SUBSTRING(Data,274,4) RESIDENCE_ZIP4,
			SUBSTRING(Data,278,10) PHONE,
			SUBSTRING(Data,288,2) OTHER_INSURER1,
			SUBSTRING(Data,290,20) OTH_POLICY1,
			SUBSTRING(Data,310,2) OTHER_INSURER2,
			SUBSTRING(Data,312,20) OTH_POLICY2,
			SUBSTRING(Data,332,2) OTHER_INSURER3,
			SUBSTRING(Data,334,20) OTH_POLICY3,
			SUBSTRING(Data,354,2) MEMBERS,
			SUBSTRING(Data,356,2) ODSI_MEMBERS_ELIGIBLE,
			SUBSTRING(Data,358,6) USER_CODE,
			SUBSTRING(Data,364,8) ENTRY_DATE,
			SUBSTRING(Data,372,3) PCT_OF_POVERTY_LEVEL,
			SUBSTRING(Data,375,1) DEDUCTIBLE_LEVEL_CODE,
			SUBSTRING(Data,376,2) HCRE_MEMBERS_ELIGIBLE,
			SUBSTRING(Data,378,2) HCRE_DENIAL_CODE,
			SUBSTRING(Data,380,2) CARRIER_CODE,
			SUBSTRING(Data,382,8) EFFECTIVE_CARRIER_DATE,
			SUBSTRING(Data,390,10) ELA_ERRORS,
			SUBSTRING(Data,400,1) MANCOMUNADO,
			SUBSTRING(Data,401,3) FILLER,
			SUBSTRING(Data,404,9) PMG_TAX_ID,
			SUBSTRING(Data,413,2) NEW_CARRIER,
			SUBSTRING(Data,415,9) NEW_PMG_TAX_ID,
			SUBSTRING(Data,424,8) NEW_PMG_EFF_DATE,
			SUBSTRING(Data,432,13) CONTRACT_NUMBER,
			SUBSTRING(Data,445,1) REGION_ASES,
			SUBSTRING(Data,446,8) NEW_CARRIER_EFFECTIVE_DATE,
			SUBSTRING(Data,454,8) PMG_EFF_DATE,
			SUBSTRING(Data,462,8) CERTIFICATION_DATE,
			SUBSTRING(Data,470,2) PRIMARY_CENTER_PCP_CHANGE_REASON,
			SUBSTRING(Data,472,1) AUTO_ENROLL_INDICATOR,
			SUBSTRING(Data,473,8) AUTO_ENROLL_DATE,
			SUBSTRING(Data,481,11) PAM_NEW_FAMILY_ID,
			SUBSTRING(Data,492,10) APPLICATION_NUMBER,
			SUBSTRING(Data,502,8) MEDICAID_CANCELLATION_DT,
			SUBSTRING(Data,510,8) REGION_MOVE_EFF_DT,
			SUBSTRING(Data,518,2) RATE_CELL,
			SUBSTRING(Data,520,1) GENDER,
			SUBSTRING(Data,521,8) NEW_CARD_ID_DATE,
			SUBSTRING(Data,529,11) FILLER
		 FROM #TEMP;
	END TRY
	BEGIN CATCH
		--ROLLBACK TRAN @TransactionName
		SELECT
		 @ProcessType AS ProcessType
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