CREATE PROCEDURE SP_EliminarCalificacion

     @IdCalificacion UNIQUEIDENTIFIER
AS
BEGIN
   DELETE FROM Calificaciones WHERE IdCalificacion = @IdCalificacion
END;