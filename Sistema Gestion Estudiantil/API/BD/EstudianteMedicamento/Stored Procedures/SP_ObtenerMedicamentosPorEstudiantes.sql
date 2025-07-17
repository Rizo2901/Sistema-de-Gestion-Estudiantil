CREATE PROCEDURE EstudianteMedicamento.SP_ObtenerMedicamentosPorEstudiantes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        E.IdEstudiante,
        E.Cedula,
        CONCAT(E.Nombre, ' ', E.Apellido1, ' ', E.Apellido2) AS NombreCompleto,
        E.GradoActual,
        E.Genero,

        (
            SELECT 
                EM.IdEstudianteMedicamento,
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
            WHERE EM.IdEstudiante = E.IdEstudiante
            FOR JSON PATH
        ) AS Medicamentos
    FROM dbo.Estudiantes E
    WHERE EXISTS (
        SELECT 1 FROM dbo.Estudiante_Medicamento EM WHERE EM.IdEstudiante = E.IdEstudiante
    )
    ORDER BY NombreCompleto;
END