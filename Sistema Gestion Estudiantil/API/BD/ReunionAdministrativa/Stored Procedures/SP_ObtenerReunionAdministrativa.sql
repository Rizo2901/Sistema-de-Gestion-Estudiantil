
 CREATE PROCEDURE [ReunionAdministrativa].SP_ObtenerReunionAdministrativa

    @IdReunionAdmin UNIQUEIDENTIFIER
AS 
BEGIN
      
	  SELECT * FROM  ReunionesAdministrativas
	  WHERE IdReunionAdmin = @IdReunionAdmin
	  

	  
	  
END;