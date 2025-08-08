
CREATE PROCEDURE [Entidad].[ActualizarIdUsuarioProfesor]
    @IdProfesor UNIQUEIDENTIFIER,
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Profesores
    SET IdUsuario = @IdUsuario
    WHERE IdProfesor = @IdProfesor;
END