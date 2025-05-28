-- Procedimiento: Eliminar Sección
CREATE PROCEDURE SP_Eliminar_Seccion
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Secciones
    WHERE IdSeccion = @IdSeccion;
END;