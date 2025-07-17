CREATE   PROCEDURE [EstudianteMedicamento].[SP_ObtenerMedicamentosPorEstudiante]
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        EM.IdMedicamento,
        M.Nombre,
        M.Descripcion,
        EM.Dosis,
        EM.Frecuencia,
        EM.FechaInicio,
        EM.FechaFin,
        EM.Observaciones
    FROM Estudiante_Medicamento EM
    JOIN Medicamentos M ON EM.IdMedicamento = M.IdMedicamento
    WHERE EM.IdEstudiante = @IdEstudiante;
END;