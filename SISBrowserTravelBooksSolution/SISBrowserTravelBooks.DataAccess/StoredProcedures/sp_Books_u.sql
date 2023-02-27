CREATE OR ALTER PROC sp_Books_u
@Id_book int,
@Id_editorial int,
@Tittle_book varchar(50),
@Synopsis_book varchar(500)
AS
BEGIN
	UPDATE Books SET
	Id_editorial = @Id_editorial, 
	Tittle_book = @Tittle_book, 
	Synopsis_book = @Synopsis_book
	WHERE Id_book = @Id_book
END