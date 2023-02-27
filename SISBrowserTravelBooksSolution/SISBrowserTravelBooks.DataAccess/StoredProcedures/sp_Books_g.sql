CREATE OR ALTER PROC sp_Books_g
@Type_search varchar(50),
@Value_search varchar(50)
AS
BEGIN
	IF (@Type_search = 'ID BOOK')
	BEGIN
		SELECT *
		FROM Books bo
		WHERE bo.Id_book = CONVERT(int, @Value_search)
	END
	ELSE IF (@Type_search = 'ALL')
	BEGIN
		SELECT *
		FROM Books bo
	END
	ELSE IF (@Type_search = 'ID AUTHOR')
	BEGIN
		SELECT *
		FROM Books bo
		INNER JOIN Authors_books aubo ON bo.Id_book = aubo.Id_book
		INNER JOIN Authors au ON aubo.Id_author = au.Id_author
		WHERE aubo.Id_author = CONVERT(int, @Value_search)
	END
END