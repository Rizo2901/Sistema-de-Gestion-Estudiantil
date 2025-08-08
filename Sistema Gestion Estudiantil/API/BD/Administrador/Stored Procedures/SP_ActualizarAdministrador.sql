
-- 4. Crear procedimiento: ActualizarAdministrador
CREATE PROCEDURE [Administrador].[SP_ActualizarAdministrador]
    @IdAdministrador UNIQUEIDENTIFIER,
    @Nombre VARCHAR(25),
	@Cedula VARCHAR(40),
    @Apellido1 VARCHAR(25),
    @Apellido2 VARCHAR(25),
    @Direccion VARCHAR(MAX),
    @TelefonoCelular VARCHAR(25) = NULL,
    @CorreoElectronico VARCHAR(100) = NULL,
    @IdUsuario UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Administradores]
    SET
        Nombre = @Nombre,
		Cedula = @Cedula,
        Apellido1 = @Apellido1,
        Apellido2 = @Apellido2,
        Direccion = @Direccion,
        TelefonoCelular = @TelefonoCelular,
        CorreoElectronico = @CorreoElectronico,
        IdUsuario = @IdUsuario
    WHERE IdAdministrador = @IdAdministrador;
END