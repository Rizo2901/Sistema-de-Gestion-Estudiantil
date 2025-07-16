CREATE PROCEDURE SP_ObtenerComportamientoPorId @IdComportamiento UNIQUEIDENTIFIER
AS
BEGIN
    SELECT IdComportamiento, IdEstudiante, Fecha, Observacion, AccionTomada
    FROM Comportamiento
    WHERE IdComportamiento = @IdComportamiento
END