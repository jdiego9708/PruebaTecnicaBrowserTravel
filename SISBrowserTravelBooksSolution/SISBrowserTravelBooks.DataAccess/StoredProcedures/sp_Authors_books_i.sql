CREATE OR ALTER PROC sp_Authors_books_i
@Id_author int,
@Id_book int
AS
BEGIN
	INSERT INTO Authors_books
	VALUES (@Id_author, @Id_book)
END