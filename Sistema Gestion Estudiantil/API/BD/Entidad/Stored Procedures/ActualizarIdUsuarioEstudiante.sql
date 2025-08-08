
CREATE PROCEDURE [Entidad].[ActualizarIdUsuarioEstudiante]
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Estudiantes
    SET IdUsuario = @IdUsuario
    WHERE IdEstudiante = @IdEstudiante;
END