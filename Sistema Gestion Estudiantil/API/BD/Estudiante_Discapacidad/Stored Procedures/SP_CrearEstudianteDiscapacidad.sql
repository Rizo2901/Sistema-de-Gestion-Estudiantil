CREATE   PROCEDURE [Estudiante_Discapacidad].[SP_CrearEstudianteDiscapacidad]
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdDiscapacidad UNIQUEIDENTIFIER,
    @FechaDiagnostico DATE,
    @GradoAfectacion VARCHAR(50),
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Estudiante_Discapacidad (
        IdEstudianteDiscapacidad, IdEstudiante, IdDiscapacidad,
        FechaDiagnostico, GradoAfectacion, Observaciones
    )
    VALUES (
        @NuevoId, @IdEstudiante, @IdDiscapacidad,
        @FechaDiagnostico, @GradoAfectacion, @Observaciones
    );

    SELECT @NuevoId AS IdEstudianteDiscapacidad;
END;