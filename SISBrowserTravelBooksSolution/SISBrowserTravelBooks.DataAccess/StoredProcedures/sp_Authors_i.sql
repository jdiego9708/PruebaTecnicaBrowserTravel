CREATE OR ALTER PROC sp_Authors_i
@Id_author int output,
@Name_author varchar(50),
@Last_name_author varchar(50)
AS
BEGIN
	INSERT INTO Authors
	VALUES (@Name_author, @Last_name_author)

	SET @Id_author = SCOPE_IDENTITY();
END