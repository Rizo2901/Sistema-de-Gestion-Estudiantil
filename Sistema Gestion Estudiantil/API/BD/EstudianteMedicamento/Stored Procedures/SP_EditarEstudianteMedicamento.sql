
-- =============================
-- EDITAR
-- =============================
CREATE   PROCEDURE [EstudianteMedicamento].SP_EditarEstudianteMedicamento
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdMedicamento UNIQUEIDENTIFIER,
    @Dosis VARCHAR(50),
    @Frecuencia VARCHAR(50),
    @FechaInicio DATE,
    @FechaFin DATE,
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Estudiante_Medicamento
    SET Dosis = @Dosis,
        Frecuencia = @Frecuencia,
        FechaInicio = @FechaInicio,
        FechaFin = @FechaFin,
        Observaciones = @Observaciones
    WHERE IdEstudiante = @IdEstudiante AND IdMedicamento = @IdMedicamento;
END;