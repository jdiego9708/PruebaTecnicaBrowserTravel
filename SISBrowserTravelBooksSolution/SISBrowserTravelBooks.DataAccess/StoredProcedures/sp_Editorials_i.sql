CREATE OR ALTER PROC sp_Editorials_i
@Id_editorial int output,
@Name_editorial varchar(50),
@Campus_editorial varchar(50)
AS
BEGIN
	INSERT INTO Editorials
	VALUES (@Name_editorial, @Campus_editorial)

	SET @Id_editorial = SCOPE_IDENTITY();
END