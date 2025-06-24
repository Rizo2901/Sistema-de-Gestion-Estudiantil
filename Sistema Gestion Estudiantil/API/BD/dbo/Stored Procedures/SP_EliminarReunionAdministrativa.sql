 CREATE PROCEDURE SP_EliminarReunionAdministrativa

    @IdReunionAdmin UNIQUEIDENTIFIER
AS 
BEGIN
      
	  DELETE FROM  ReunionesAdministrativas
	  WHERE IdReunionAdmin = @IdReunionAdmin
	  

	  
	  
END;