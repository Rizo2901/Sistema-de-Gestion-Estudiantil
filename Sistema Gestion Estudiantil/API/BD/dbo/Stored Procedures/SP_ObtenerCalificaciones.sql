CREATE PROCEDURE SP_ObtenerCalificaciones

AS
BEGIN 
     SELECT IdCalificacion,IdMateria, Calificacion, Fecha
	 from Calificaciones;
END;