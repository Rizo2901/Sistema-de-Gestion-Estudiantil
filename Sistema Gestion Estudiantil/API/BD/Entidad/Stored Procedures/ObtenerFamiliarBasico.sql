CREATE PROCEDURE [Entidad].[ObtenerFamiliarBasico]
    @IdFamiliar UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Nombre,
		Cedula,
        Apellido1,
        Apellido2,
        CorreoElectronico
    FROM Familiares
    WHERE IdFamiliar = @IdFamiliar;
END