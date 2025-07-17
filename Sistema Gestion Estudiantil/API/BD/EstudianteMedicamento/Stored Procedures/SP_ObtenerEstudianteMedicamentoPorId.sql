
CREATE PROCEDURE EstudianteMedicamento.SP_ObtenerEstudianteMedicamentoPorId
    @IdEstudianteMedicamento UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        EM.IdEstudianteMedicamento,
        EM.IdEstudiante,
        EM.IdMedicamento,
        M.Nombre AS NombreMedicamento,
        M.Descripcion AS DescripcionMedicamento,
        EM.Dosis,
        EM.Frecuencia,
        EM.FechaInicio,
        EM.FechaFin,
        EM.Observaciones
    FROM dbo.Estudiante_Medicamento EM
    JOIN dbo.Medicamentos M ON EM.IdMedicamento = M.IdMedicamento
    WHERE EM.IdEstudianteMedicamento = @IdEstudianteMedicamento;
END