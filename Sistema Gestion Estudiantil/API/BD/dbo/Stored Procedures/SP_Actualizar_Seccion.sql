-- Procedimiento: Actualizar Sección
CREATE PROCEDURE SP_Actualizar_Seccion
    @IdSeccion UNIQUEIDENTIFIER,
    @NuevoNombre VARCHAR(50)
AS
BEGIN
    UPDATE Secciones
    SET Nombre = @NuevoNombre
    WHERE IdSeccion = @IdSeccion;
END;