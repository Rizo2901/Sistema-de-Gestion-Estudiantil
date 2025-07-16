CREATE PROCEDURE SP_ObtenerComportamientos
AS
BEGIN
    SELECT IdComportamiento, IdEstudiante, Fecha, Observacion, AccionTomada
    FROM Comportamiento
END