 CREATE PROCEDURE SP_ObtenerReunionConPadre
    
	 @IdReunion UNIQUEIDENTIFIER 

AS
BEGIN

SELECT * FROM Reuniones WHERE IdReunion = @IdReunion

END;