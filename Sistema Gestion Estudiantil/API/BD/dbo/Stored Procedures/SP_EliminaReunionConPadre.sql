CREATE PROCEDURE SP_EliminaReunionConPadre
  @IdReunion UNIQUEIDENTIFIER

AS
BEGIN

 DELETE FROM Reuniones 
 WHERE IdReunion = @IdReunion

 END