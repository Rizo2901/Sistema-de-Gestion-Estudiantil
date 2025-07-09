CREATE PROCEDURE [Actividad].[SP_ObtenerActividadPorId]
    @IdActividad UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM Actividades
    WHERE IdActividad = @IdActividad;
END;