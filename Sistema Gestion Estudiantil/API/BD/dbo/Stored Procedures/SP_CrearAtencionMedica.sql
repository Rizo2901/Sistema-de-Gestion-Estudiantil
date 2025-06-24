-- CREATE
CREATE PROCEDURE SP_CrearAtencionMedica
    @IdEstudiante UNIQUEIDENTIFIER,
    @Fecha DATE,
    @Diagnostico TEXT,
    @Tratamiento TEXT = NULL
AS
BEGIN
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();
    INSERT INTO AtencionesMedicas (IdAtencionMedica,IdEstudiante, Fecha, Diagnostico, Tratamiento)
    VALUES (@NuevoId,@IdEstudiante, @Fecha, @Diagnostico, @Tratamiento)

	SELECT @NuevoId as IdAtencionMedica;
END