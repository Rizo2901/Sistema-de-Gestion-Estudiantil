CREATE PROCEDURE SeccionProfesor.SP_ObtenerProfesoresPorSeccion
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        P.IdProfesor,
        P.Nombre AS NombreProfesor,
        P.Apellido1 AS Apellido1Profesor,
        P.Apellido2 AS Apellido2Profesor
    FROM dbo.SeccionProfesor SP
    JOIN Profesores P ON SP.IdProfesor = P.IdProfesor
    WHERE SP.IdSeccion = @IdSeccion;
END