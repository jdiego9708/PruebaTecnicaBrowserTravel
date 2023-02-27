CREATE OR ALTER PROC sp_Books_i
@Id_book int output,
@Id_editorial int,
@Tittle_book varchar(50),
@Synopsis_book varchar(500)
AS
BEGIN
	INSERT INTO Books
	VALUES (@Id_editorial, @Tittle_book, @Synopsis_book)

	SET @Id_book = SCOPE_IDENTITY();
END