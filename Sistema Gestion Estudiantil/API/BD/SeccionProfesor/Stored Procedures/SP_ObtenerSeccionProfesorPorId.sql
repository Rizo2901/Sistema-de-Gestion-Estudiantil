CREATE PROCEDURE SeccionProfesor.SP_ObtenerSeccionProfesorPorId
    @IdProfesorSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        SP.IdProfesorSeccion,
        SP.IdProfesor,
        P.Nombre AS NombreProfesor,
        P.Apellido1 AS Apellido1Profesor,
        P.Apellido2 AS Apellido2Profesor,
        SP.IdSeccion,
        S.Nombre AS NombreSeccion
    FROM dbo.SeccionProfesor SP
    JOIN Profesores P ON SP.IdProfesor = P.IdProfesor
    JOIN Secciones S ON SP.IdSeccion = S.IdSeccion
    WHERE SP.IdProfesorSeccion = @IdProfesorSeccion;
END