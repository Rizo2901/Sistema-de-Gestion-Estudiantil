
-- 6. Activar o Desactivar Usuario
CREATE PROCEDURE [Usuario].[CambiarEstadoUsuario]
    @IdUsuario UNIQUEIDENTIFIER,
    @NuevoEstado BIT -- 1 = Activo, 0 = Inactivo
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Usuarios]
    SET
        Estado = @NuevoEstado,
        FechaActivacion = CASE WHEN @NuevoEstado = 1 THEN GETDATE() ELSE NULL END
    WHERE IdUsuario = @IdUsuario;
END