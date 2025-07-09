CREATE PROCEDURE [Seccion].[SP_EditarSeccion]
    @IdSeccion UNIQUEIDENTIFIER,
    @NuevoNombre VARCHAR(50)
AS
BEGIN
    UPDATE Secciones
    SET Nombre = @NuevoNombre
    WHERE IdSeccion = @IdSeccion;
END;