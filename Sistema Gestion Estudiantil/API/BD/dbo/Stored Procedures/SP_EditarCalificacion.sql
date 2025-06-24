CREATE PROCEDURE [dbo].[SP_EditarCalificacion] 
    @IdCalificacion UNIQUEIDENTIFIER,
    @IdEstudiante   UNIQUEIDENTIFIER ,
    @IdMateria      UNIQUEIDENTIFIER ,
    @Calificacion   DECIMAL(5, 2) ,
    @Fecha          DATE ,
	@Comentario     VARCHAR(MAX)
AS
BEGIN
      SET NOCOUNT ON;
	  UPDATE Calificaciones
	  SET IdEstudiante = @IdEstudiante,
	      IdMateria = @IdMateria,
		  Calificacion = @Calificacion,
		  Fecha = @Fecha,
		  Comentario = @Comentario
	  WHERE IdCalificacion = @IdCalificacion;


END