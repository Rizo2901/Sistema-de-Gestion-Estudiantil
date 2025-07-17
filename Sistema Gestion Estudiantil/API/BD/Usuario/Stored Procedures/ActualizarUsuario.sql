
-- 4. Actualizar Usuario
CREATE PROCEDURE [Usuario].[ActualizarUsuario]
    @IdUsuario UNIQUEIDENTIFIER,
    @NombreUsuario VARCHAR(50),
    @Contrasena VARCHAR(255),
    @CorreoElectronico VARCHAR(100) = NULL,
    @IdRol UNIQUEIDENTIFIER,
    @TipoEntidad VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Usuarios]
    SET
        NombreUsuario = @NombreUsuario,
        Contrasena = @Contrasena,
        CorreoElectronico = @CorreoElectronico,
        IdRol = @IdRol,
        TipoEntidad = @TipoEntidad
    WHERE IdUsuario = @IdUsuario;
END