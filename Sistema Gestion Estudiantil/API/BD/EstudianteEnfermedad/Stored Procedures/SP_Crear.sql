CREATE   PROCEDURE EstudianteEnfermedad.SP_Crear
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdEnfermedad UNIQUEIDENTIFIER,
    @FechaDiagnostico DATE,
    @Gravedad VARCHAR(50),
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Estudiante_Enfermedad (
        IdEstudianteEnfermedad, IdEstudiante, IdEnfermedad,
        FechaDiagnostico, Gravedad, Observaciones
    )
    VALUES (
        @NuevoId, @IdEstudiante, @IdEnfermedad,
        @FechaDiagnostico, @Gravedad, @Observaciones
    );

    SELECT @NuevoId AS IdEstudianteEnfermedad;
END;