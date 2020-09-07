CREATE FUNCTION RemoveExtraSpacesColumn(@str VARCHAR(MAX))
RETURNS VARCHAR(MAX) AS
BEGIN 
	SET @str = TRIM(@str)
    WHILE CHARINDEX('  ', @str) > 0 
        SET @str = REPLACE(@str, '  ', ' ')

    RETURN @str
END