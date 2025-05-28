-- Procedimiento: Obtener Sección por Id
CREATE PROCEDURE SP_Obtener_Seccion_Por_Id
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SELECT IdSeccion, Nombre
    FROM Secciones
    WHERE IdSeccion = @IdSeccion;
END;