 CREATE PROCEDURE [ReunionAdministrativa].[SP_EditarReunionAdministrativa]

    @IdReunionAdmin UNIQUEIDENTIFIER,
    @Fecha DATETIME ,
	@Tema  VARCHAR(255),
	@Descripcion TEXT,
	@Lugar VARCHAR(100),
	@Responsable UNIQUEIDENTIFIER
AS 
BEGIN
      
	  SET NOCOUNT ON;
	  UPDATE ReunionesAdministrativas

	  SET IdReunionAdmin = @IdReunionAdmin,
	  Fecha = @Fecha,
	  Tema = @Tema,
	  Descripcion = @Descripcion,
	  Lugar = @Lugar,
	  Responsable = @Responsable
	  WHERE IdReunionAdmin = @IdReunionAdmin
	  

	  
	  
END;