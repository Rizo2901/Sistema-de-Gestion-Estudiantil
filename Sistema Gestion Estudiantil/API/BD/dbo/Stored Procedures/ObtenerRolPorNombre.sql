CREATE PROCEDURE ObtenerRolPorNombre
    @NombreRol VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        IdRol,
        Nombre
    FROM Roles
    WHERE Nombre = @NombreRol 
END