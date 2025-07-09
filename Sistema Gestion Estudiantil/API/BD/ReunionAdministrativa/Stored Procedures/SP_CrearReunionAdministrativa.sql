 CREATE PROCEDURE [ReunionAdministrativa].[SP_CrearReunionAdministrativa]

    @Fecha DATETIME ,
	@Tema  VARCHAR(255),
	@Descripcion TEXT,
	@Lugar VARCHAR(100),
	@Responsable UNIQUEIDENTIFIER NULL
AS 
BEGIN
      DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();


	  INSERT INTO ReunionesAdministrativas (IdReunionAdmin, Fecha, Tema, Descripcion, Lugar, Responsable)
	  VALUES (@NuevoId,  @Fecha, @Tema, @Descripcion, @Lugar, @Responsable);

	  SELECT @NuevoId AS IdReunionAdmin;
END;