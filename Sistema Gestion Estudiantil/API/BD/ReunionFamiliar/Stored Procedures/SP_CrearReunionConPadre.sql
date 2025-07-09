CREATE PROCEDURE [ReunionFamiliar].[SP_CrearReunionConPadre]

    @IdEstudiante UNIQUEIDENTIFIER ,
    @Fecha          DATETIME ,
	@Motivo  TEXT

AS 
BEGIN
      DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();


	  INSERT INTO Reuniones (IdReunion, IdEstudiante, Fecha, Motivo)
	  VALUES (@NuevoId, @IdEstudiante, @Fecha, @Motivo);

	  SELECT @NuevoId AS IdReunion;
END;