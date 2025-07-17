
CREATE PROCEDURE [Usuario].[CrearUsuario]
    @NombreUsuario VARCHAR(50),
    @Contrasena VARCHAR(255),
    @CorreoElectronico VARCHAR(100) = NULL,
    @IdRol UNIQUEIDENTIFIER,
    @TipoEntidad VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
	 DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[Usuarios] (
        IdUsuario,NombreUsuario, Contrasena, CorreoElectronico,
        IdRol, Estado, FechaActivacion, TipoEntidad
    )
    VALUES (
        @NuevoId,@NombreUsuario, @Contrasena, @CorreoElectronico,
        @IdRol, 1, GETDATE(), @TipoEntidad
    );
END