CREATE OR ALTER PROC sp_Editorials_g
@Type_search varchar(50),
@Value_search varchar(50)
AS
BEGIN
	IF (@Type_search = 'ID EDITORIAL')
	BEGIN
		SELECT *
		FROM Editorials ed
		WHERE ed.Id_editorial = CONVERT(int, @Value_search)
	END
	ELSE IF (@Type_search = 'CAMPUS EDITORIAL')
	BEGIN
		SELECT *
		FROM Editorials ed
		WHERE ed.Campus_editorial = @Value_search
	END
		ELSE IF (@Type_search = 'ALL')
	BEGIN
		SELECT *
		FROM Editorials ed
	END
END