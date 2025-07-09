
-- =============================
-- EDITAR
-- =============================
CREATE   PROCEDURE EstudianteEnfermedad.SP_Editar
    @IdEstudianteEnfermedad UNIQUEIDENTIFIER,
    @FechaDiagnostico DATE,
    @Gravedad VARCHAR(50),
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Estudiante_Enfermedad
    SET FechaDiagnostico = @FechaDiagnostico,
        Gravedad = @Gravedad,
        Observaciones = @Observaciones
    WHERE IdEstudianteEnfermedad = @IdEstudianteEnfermedad;
END;