
CREATE PROCEDURE [Usuario].[ActualizarContrasena]
    @IdUsuario UNIQUEIDENTIFIER,
    @NuevaContrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Usuarios]
    SET Contrasena = @NuevaContrasena
    WHERE IdUsuario = @IdUsuario;
END