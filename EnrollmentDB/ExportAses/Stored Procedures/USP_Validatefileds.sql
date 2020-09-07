-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [ExportAses].[USP_Validatefileds]

	@TableName VARCHAR(50),
	@ProcessHeaderId INT
AS
BEGIN
	DECLARE @UpdateField VARCHAR(500)
	DECLARE @PartUpdateField VARCHAR(100)
	DECLARE @ID INT
	DECLARE @ColumnName VARCHAR(30)
	DECLARE @DataType VARCHAR(10)
	DECLARE @Length INT
	DECLARE @TableLength INT
	DECLARE @ValueInt INT
	DECLARE @Sql NVARCHAR(200)

	DECLARE DataCursor CURSOR FOR
	SELECT TOP 1000 Id FROM [ExportAses].[NetworkProvidersImport] WHERE ProcessHeaderId = @ProcessHeaderId

	OPEN DataCursor
	--PRINT @@CURSOR_ROWS -- WHY NOT WORKS??
	FETCH NEXT FROM DataCursor INTO @ID 

	WHILE ( @@FETCH_STATUS = 0 )
    BEGIN
		SET @UpdateField = '|'
		DECLARE FieldCursor CURSOR FOR
		SELECT ColumnName, DataType, CAST(COALESCE(Length, 0) AS INT) FROM [ExportAses].[ConfigurationTableImport] WHERE TableName = @TableName AND Enabled = 1

		OPEN FieldCursor 
		FETCH NEXT FROM FieldCursor INTO @ColumnName, @DataType, @Length
		
		WHILE ( @@FETCH_STATUS = 0 )
		BEGIN
			
			SET @Sql = 'SELECT @TableLength = CAST(COALESCE(LEN(' + @ColumnName + '), 0) AS INT) FROM [ExportAses].[NetworkProvidersImport] WHERE Id = ' + CAST(@ID as VARCHAR(10))
			EXEC sp_executesql @Sql, N'@TableLength INT OUT', @TableLength OUT

			IF(@DataType = 'INT')
			BEGIN
				SET @Sql = 'SELECT @ValueInt = CAST(' + @ColumnName + ' AS INT) FROM [ExportAses].[NetworkProvidersImport] WHERE Id = ' + CAST(@ID as VARCHAR(10))
				EXEC sp_executesql @Sql, N'@ValueInt INT OUT', @ValueInt OUT
				 
				--PRINT @ColumnName + ' - ' + @DataType + ' - ' + CAST(@ValueInt AS VARCHAR(10))
				IF(ISNUMERIC(@ValueInt) = 0)
				BEGIN
					SET @PartUpdateField = 'Column: ' + @ColumnName + ' is not numeric.|'
					SET @UpdateField = CONCAT(@UpdateField, @PartUpdateField)
				END
			END
			IF(@DataType = 'NCHAR')
			BEGIN
				--PRINT @ColumnName + ' - ' + @DataType + ' - ' + CAST(@TableLength AS VARCHAR(10)) + ' - ' + CAST(@Length AS VARCHAR(10))
				IF(@TableLength != @Length)
				BEGIN
					SET @PartUpdateField = 'Column: ' + @ColumnName + ' not have the correct number of characters.|'
					SET @UpdateField = CONCAT(@UpdateField, @PartUpdateField)
				END
			END
			IF(@DataType = 'NVARCHAR')
			BEGIN
				--PRINT @ColumnName + ' - ' + @DataType + ' - ' + CAST(@TableLength AS VARCHAR(10)) + ' - ' + CAST(@Length AS VARCHAR(10))
				IF(@TableLength > @Length)
				BEGIN
					SET @PartUpdateField = 'Column: ' + @ColumnName + ' have more characters than allowed.|'
					SET @UpdateField = CONCAT(@UpdateField, @PartUpdateField)
				END
			END
			
			FETCH NEXT FROM FieldCursor INTO @ColumnName, @DataType, @Length
			
		END

		PRINT @UpdateField
		IF(@UpdateField != '|')
		BEGIN
			UPDATE [ExportAses].[NetworkProvidersImport]
			SET 
				MessageInvalid = @UpdateField,
				IsValidForImport = 0
			WHERE Id = @ID
		END

		CLOSE FieldCursor 
		DEALLOCATE FieldCursor

		FETCH NEXT FROM DataCursor INTO @ID
    END

	CLOSE DataCursor 
	DEALLOCATE DataCursor
END