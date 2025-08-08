
CREATE PROCEDURE [Entidad].[ActualizarIdUsuarioAdministrador]
    @IdAdministrador UNIQUEIDENTIFIER,
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Administradores
    SET IdUsuario = @IdUsuario
    WHERE IdAdministrador = @IdAdministrador;
END