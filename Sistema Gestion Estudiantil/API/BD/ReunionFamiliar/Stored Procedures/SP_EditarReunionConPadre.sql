
CREATE PROCEDURE [ReunionFamiliar].[SP_EditarReunionConPadre]
    @IdReunion UNIQUEIDENTIFIER ,
    @IdEstudiante UNIQUEIDENTIFIER ,
    @Fecha          DATETIME ,
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