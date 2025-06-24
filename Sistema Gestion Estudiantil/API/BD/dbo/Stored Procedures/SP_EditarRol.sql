CREATE PROCEDURE SP_EditarRol
    @IdRol UNIQUEIDENTIFIER,
    @Nombre VARCHAR(50),
    @Descripcion VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Roles
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE IdRol = @IdRol;
END;