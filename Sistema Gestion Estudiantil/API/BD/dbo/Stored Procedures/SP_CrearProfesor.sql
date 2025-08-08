
CREATE PROCEDURE [dbo].[SP_CrearProfesor] (
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
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Profesores (
        IdProfesor,
        Nombre,Cedula, Apellido1, Apellido2, Direccion, TelefonoCelular, CorreoElectronico,
        IdUsuario
    )
    VALUES (
        @NuevoId,
         @Nombre, @Cedula,@Apellido1, @Apellido2,  @Direccion, @TelefonoCelular, @CorreoElectronico,
         @IdUsuario
    );

    SELECT @NuevoId AS IdProfesor;
END;