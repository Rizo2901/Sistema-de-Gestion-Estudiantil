
CREATE PROCEDURE [dbo].[SP_CrearCalificacion] 

    @IdEstudiante   UNIQUEIDENTIFIER ,
    @IdMateria      UNIQUEIDENTIFIER ,
    @Calificacion   DECIMAL(5, 2) ,
    @Fecha          DATE ,
	@Comentario     VARCHAR(MAX)

AS
BEGIN
      DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();


	  INSERT INTO Calificaciones (IdCalificacion, IdEstudiante, IdMateria,Calificacion,Fecha,Comentario)
	  VALUES (@NuevoId, @IdEstudiante, @IdMateria, @Calificacion, @Fecha, @Comentario);

	  SELECT @NuevoId AS IdCalificacion;

END