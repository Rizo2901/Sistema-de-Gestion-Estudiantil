CREATE PROCEDURE [Administrador].[SP_CrearAdministrador]
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
    
	DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[Administradores] (
	 IdAdministrador, Nombre,Cedula, Apellido1, Apellido2, Direccion,
        TelefonoCelular, CorreoElectronico, IdUsuario
    )
    VALUES (
        @NuevoId,@Nombre,@Cedula, @Apellido1, @Apellido2, @Direccion,
        @TelefonoCelular, @CorreoElectronico, @IdUsuario
    );

	SELECT @NuevoId AS IdAdministrador;
END