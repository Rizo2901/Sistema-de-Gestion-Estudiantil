
CREATE PROCEDURE SP_EditarReunionConPadre
    @IdReunion UNIQUEIDENTIFIER ,
    @IdEstudiante UNIQUEIDENTIFIER ,
    @Fecha          DATE ,
	@Motivo  TEXT

AS 
BEGIN
     SET NOCOUNT ON;
	 UPDATE Reuniones

	 SET IdReunion = @IdReunion,
	     IdEstudiante = @IdEstudiante,
		 Fecha = @Fecha,
		 Motivo = @Motivo
	WHERE IdReunion = @IdReunion

END;