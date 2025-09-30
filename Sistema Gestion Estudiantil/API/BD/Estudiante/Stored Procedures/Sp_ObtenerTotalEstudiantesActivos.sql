CREATE PROCEDURE [Estudiante].[Sp_ObtenerTotalEstudiantesActivos]
AS
BEGIN
    SELECT COUNT(*) AS TotalActivos
    FROM Estudiantes
    WHERE Estado = 1
END