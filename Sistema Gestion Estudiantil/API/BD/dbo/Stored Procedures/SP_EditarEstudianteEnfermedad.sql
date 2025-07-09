
-- EDITAR
CREATE   PROCEDURE SP_EditarEstudianteEnfermedad
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdEnfermedad UNIQUEIDENTIFIER,
    @FechaDiagnostico DATE,
    @Gravedad VARCHAR(50),
    @Observaciones TEXT
AS
BEGIN
    UPDATE Estudiante_Enfermedad
    SET FechaDiagnostico = @FechaDiagnostico,
        Gravedad = @Gravedad,
        Observaciones = @Observaciones
    WHERE IdEstudiante = @IdEstudiante AND IdEnfermedad = @IdEnfermedad;
END;