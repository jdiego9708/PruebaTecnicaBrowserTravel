CREATE OR ALTER PROC sp_Editorials_u
@Id_editorial int,
@Name_editorial varchar(50),
@Campus_editorial varchar(50)
AS
BEGIN
	UPDATE Editorials SET
	Name_editorial = @Name_editorial, 
	Campus_editorial = @Campus_editorial
	WHERE Id_editorial = @Id_editorial
END