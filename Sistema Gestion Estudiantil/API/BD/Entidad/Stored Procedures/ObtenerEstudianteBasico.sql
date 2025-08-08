CREATE PROCEDURE [Entidad].[ObtenerEstudianteBasico]
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Nombre,
		Cedula,
        Apellido1,
        Apellido2,
        CorreoElectronico,
        Cedula
    FROM Estudiantes
    WHERE IdEstudiante = @IdEstudiante;
END