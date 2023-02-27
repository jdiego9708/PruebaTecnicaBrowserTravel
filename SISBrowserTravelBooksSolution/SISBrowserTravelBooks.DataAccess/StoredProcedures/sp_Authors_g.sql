CREATE OR ALTER PROC sp_Authors_g
@Type_search varchar(50),
@Value_search varchar(50)
AS
BEGIN
	IF (@Type_search = 'ID AUTHOR')
	BEGIN
		SELECT *
		FROM Authors au
		WHERE au.Id_author = CONVERT(int, @Value_search)
	END
	ELSE IF (@Type_search = 'ALL')
	BEGIN
		SELECT *
		FROM Authors au
	END
END