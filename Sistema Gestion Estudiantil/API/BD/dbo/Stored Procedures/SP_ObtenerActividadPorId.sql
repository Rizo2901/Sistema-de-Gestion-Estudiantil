CREATE PROCEDURE SP_ObtenerActividadPorId
    @IdActividad UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM Actividades
    WHERE IdActividad = @IdActividad;
END;