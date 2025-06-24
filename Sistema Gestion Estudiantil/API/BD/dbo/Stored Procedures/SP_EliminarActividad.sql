
CREATE PROCEDURE SP_EliminarActividad
    @IdActividad UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Actividades
    WHERE IdActividad = @IdActividad;
END;