
 CREATE PROCEDURE SP_CrearReunionAdministrativa

    @Fecha DATE ,
	@Tema  VARCHAR(255),
	@Descripcion TEXT,
	@Lugar VARCHAR(100),
	@Responsable UNIQUEIDENTIFIER
AS 
BEGIN
      DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();


	  INSERT INTO ReunionesAdministrativas (IdReunionAdmin, Fecha, Tema, Descripcion, Lugar, Responsable)
	  VALUES (@NuevoId,  @Fecha, @Tema, @Descripcion, @Lugar, @Responsable);

	  SELECT @NuevoId AS IdReunionAdmin;
END;