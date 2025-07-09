
-- =============================
-- ELIMINAR
-- =============================
CREATE   PROCEDURE SP_EliminarEstudianteMedicamento
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdMedicamento UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Estudiante_Medicamento
    WHERE IdEstudiante = @IdEstudiante AND IdMedicamento = @IdMedicamento;
END;