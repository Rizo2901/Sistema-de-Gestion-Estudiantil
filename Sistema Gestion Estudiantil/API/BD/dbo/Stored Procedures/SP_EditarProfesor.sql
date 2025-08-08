
CREATE PROCEDURE [dbo].[SP_EditarProfesor](
   @IdProfesor UNIQUEIDENTIFIER,
    @Nombre VARCHAR(255),
	@Cedula VARCHAR (40),
    @Apellido1 VARCHAR(255),
    @Apellido2 VARCHAR(255),
    @Direccion TEXT = NULL,
    @TelefonoCelular VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(100) = NULL,
    @IdUsuario UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Profesores
    SET
        Nombre = @Nombre,
		Cedula = @Cedula,
        Apellido1 = @Apellido1,
        Apellido2 = @Apellido2,
        Direccion = @Direccion,
        TelefonoCelular = @TelefonoCelular,
        CorreoElectronico = @CorreoElectronico,
        IdUsuario = @IdUsuario
    WHERE IdProfesor= @IdProfesor;
END;