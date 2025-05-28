-- Procedimiento: Obtener todas las Secciones
CREATE PROCEDURE SP_Obtener_Secciones
AS
BEGIN
    SELECT IdSeccion, Nombre
    FROM Secciones;
END;