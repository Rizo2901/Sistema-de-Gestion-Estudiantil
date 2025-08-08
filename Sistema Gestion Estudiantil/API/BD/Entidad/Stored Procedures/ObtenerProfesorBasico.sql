CREATE PROCEDURE [Entidad].[ObtenerProfesorBasico]
    @IdProfesor UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Nombre,
		Cedula,
        Apellido1,
        Apellido2,
        CorreoElectronico
    FROM Profesores
    WHERE IdProfesor = @IdProfesor;
END