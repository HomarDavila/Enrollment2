-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [ExportAses].[USP_UploadDataText] 
AS
BEGIN
	CREATE TABLE #temp (data varchar(2000));
BULK INSERT #temp 
FROM 'D:\VS[SourceCode]\[Proy-Enrollment]\Files\NHM\190201enrollment.sus'
WITH 
(	
ROWTERMINATOR ='\n'
);
select 
SUBSTRING(data,1,1) RECORD_TYPE,
SUBSTRING(data,2,1) TRAN_ID,
SUBSTRING(data,3,8) PROCESS_DATE,
SUBSTRING(data,11,1) REGION,
SUBSTRING(data,12,2) CARRIER,
SUBSTRING(data,14,4) MEMBER_PRIMARY_CENTER,
SUBSTRING(data,18,11) ODSI_FAMILY_ID,
SUBSTRING(data,29,9) MEMBER_SSN,
SUBSTRING(data,38,2) MEMBER_SUFFIX,
SUBSTRING(data,40,8) EFFECTIVE_DATE,
SUBSTRING(data,48,2) PLAN_TYPE,
SUBSTRING(data,50,3) PLAN_VERSION,
SUBSTRING(data,53,13) MPI,
SUBSTRING(data,66,15) PCP1,
SUBSTRING(data,81,8) PCP1_EFFECTIVE_DATE,
SUBSTRING(data,89,15) PCP2,
SUBSTRING(data,104,8) PCP2_EFFECTIVE_DATE,
SUBSTRING(data,112,4) FAMILY_PRIMARY_CENTER,
SUBSTRING(data,116,8) PMG_tax_ID_eff_dt,
SUBSTRING(data,124,2) IPA_PCP_CHANGE_REASON,
SUBSTRING(data,126,1) MEDICARE_INDICATOR,
SUBSTRING(data,127,12) HIC_NUMBER,
SUBSTRING(data,139,1) Reject_Identifier,
SUBSTRING(data,140,14) Record_Key,
SUBSTRING(data,154,3) Error_Code_1,
SUBSTRING(data,157,3) Error_Code_2,
SUBSTRING(data,160,3) Error_Code_3,
SUBSTRING(data,163,3) Error_Code_4,
SUBSTRING(data,166,3) Error_Code_5,
SUBSTRING(data,169,3) Error_Code_6,
SUBSTRING(data,172,3) Error_Code_7,
SUBSTRING(data,175,3) Error_Code_8,
SUBSTRING(data,178,3) Error_Code_9,
SUBSTRING(data,181,3) Error_Code_10,
SUBSTRING(data,184,8) Update_Date,
SUBSTRING(data,192,8) Update_User,
SUBSTRING(data,200,1) IPA_ESPECIAL,
SUBSTRING(data,201,13) Contract_Number,
SUBSTRING(data,214,1) Special_Enroll,
SUBSTRING(data,215,9) PMG_tax_id,
SUBSTRING(data,224,2) Data_Source,
SUBSTRING(data,226,4) Filler
 from #temp;
If(OBJECT_ID('EnrollmentDB.#temp') Is Not Null)
Begin
    Drop Table #Temp
End
END