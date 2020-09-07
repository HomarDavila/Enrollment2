USE [EnrollmentDB2]
--DECLARE @str VARCHAR(MAX) = '  TXT1 TXT2  TXT3   TXT4   TXT5     TXT6                   ';
--SET @str = TRIM(@str)
--SELECT @str
--WHILE CHARINDEX('  ', @str) > 0 
--    SET @str = REPLACE(@Str, '  ', ' ')


/*------------------------------------------
				[1]
------------------------------------------*/
CREATE FUNCTION RemoveExtraSpacesColumn(@str VARCHAR(MAX))
RETURNS VARCHAR(MAX) AS
BEGIN 
	SET @str = TRIM(@str)
    WHILE CHARINDEX('  ', @str) > 0 
        SET @str = REPLACE(@str, '  ', ' ')

    RETURN @str
END


/*------------------------------------------
				 [2]
------------------------------------------*/
UPDATE [Enrollment].[PersonPrimaryCarePhysician]
SET [FullName] = [dbo].[RemoveExtraSpacesColumn]([FullName]);


/*------------------------------------------
				 [3]
------------------------------------------*/
DROP FUNCTION [dbo].[RemoveExtraSpacesColumn]