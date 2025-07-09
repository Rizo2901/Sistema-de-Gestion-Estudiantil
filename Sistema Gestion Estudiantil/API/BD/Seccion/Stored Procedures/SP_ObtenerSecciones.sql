CREATE PROCEDURE [Seccion].[SP_ObtenerSecciones]
AS
BEGIN
    SELECT IdSeccion, Nombre
    FROM Secciones;
END;