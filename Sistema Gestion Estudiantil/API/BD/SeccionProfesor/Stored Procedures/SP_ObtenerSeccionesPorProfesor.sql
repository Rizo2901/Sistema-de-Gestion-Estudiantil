CREATE PROCEDURE SeccionProfesor.SP_ObtenerSeccionesPorProfesor
    @IdProfesor UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        S.IdSeccion,
        S.Nombre AS NombreSeccion
    FROM dbo.SeccionProfesor SP
    JOIN Secciones S ON SP.IdSeccion = S.IdSeccion
    WHERE SP.IdProfesor = @IdProfesor;
END