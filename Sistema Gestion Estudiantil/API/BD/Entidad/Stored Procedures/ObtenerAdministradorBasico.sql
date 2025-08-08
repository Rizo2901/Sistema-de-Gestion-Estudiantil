CREATE PROCEDURE [Entidad].[ObtenerAdministradorBasico]
    @IdAdministrador UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Nombre,
		Cedula,
        Apellido1,
        Apellido2,
        CorreoElectronico
    FROM Administradores
    WHERE IdAdministrador = @IdAdministrador;
END