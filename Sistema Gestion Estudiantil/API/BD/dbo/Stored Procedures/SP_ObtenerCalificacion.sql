CREATE PROCEDURE SP_ObtenerCalificacion
   @IdCalificacion UNIQUEIDENTIFIER
AS
BEGIN 

SELECT * FROM Calificaciones WHERE IdCalificacion = @IdCalificacion;

END;