CREATE PROCEDURE Sp_ObtenerProfesoresTotal
AS
BEGIN
   SELECT COUNT(*) AS TotalProfesores
   FROM Profesores
END