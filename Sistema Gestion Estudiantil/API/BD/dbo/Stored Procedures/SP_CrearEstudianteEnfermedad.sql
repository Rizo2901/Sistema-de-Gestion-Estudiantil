CREATE   PROCEDURE SP_CrearEstudianteEnfermedad
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdEnfermedad UNIQUEIDENTIFIER,
    @FechaDiagnostico DATE,
    @Gravedad VARCHAR(50),
    @Observaciones TEXT
AS
BEGIN
    INSERT INTO Estudiante_Enfermedad (IdEstudiante, IdEnfermedad, FechaDiagnostico, Gravedad, Observaciones)
    VALUES (@IdEstudiante, @IdEnfermedad, @FechaDiagnostico, @Gravedad, @Observaciones);
END;