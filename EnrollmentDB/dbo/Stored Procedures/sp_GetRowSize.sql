CREATE procedure sp_GetRowSize(@Tablename varchar(100),@pkcol varchar(100))
AS 
BEGIN
declare @dynamicsql varchar(1000)

-- A @pkcol can be used to identify max/min length row
set @dynamicsql = 'select ' + @PkCol +' , (0'

-- traverse each record and calculate the datalength
select @dynamicsql = @dynamicsql + ' + isnull(datalength(' + name + '), 1)' 
	from syscolumns where id = object_id(@Tablename)
set @dynamicsql = @dynamicsql + ') as rowsize from ' + @Tablename -- + ' order by AddressID'
print @dynamicsql


exec (@dynamicsql)

END