
CREATE PROCEDURE [Entidad].[ActualizarIdUsuarioFamiliar]
    @IdFamiliar UNIQUEIDENTIFIER,
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Familiares
    SET IdUsuario = @IdUsuario
    WHERE IdFamiliar = @IdFamiliar;
END