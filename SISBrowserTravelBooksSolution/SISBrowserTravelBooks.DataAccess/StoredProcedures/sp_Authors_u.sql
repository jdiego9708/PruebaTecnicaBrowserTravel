CREATE OR ALTER PROC sp_Authors_u
@Id_author int,
@Name_author varchar(50),
@Last_name_author varchar(50)
AS
BEGIN
	UPDATE Authors SET
	Name_author = @Name_author, 
	Last_name_author = @Last_name_author
	WHERE Id_author = @Id_author
END