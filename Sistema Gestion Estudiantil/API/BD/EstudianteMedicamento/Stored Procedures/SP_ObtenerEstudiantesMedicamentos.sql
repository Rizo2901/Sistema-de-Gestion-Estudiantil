CREATE PROCEDURE EstudianteMedicamento.[SP_ObtenerEstudiantesMedicamentos]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        EM.IdEstudianteMedicamento,
        EM.IdEstudiante,
        E.Cedula,
        CONCAT(E.Nombre, ' ', E.Apellido1, ' ', E.Apellido2) AS NombreEstudiante,
        EM.IdMedicamento,
        M.Nombre AS NombreMedicamento,
        M.Descripcion AS DescripcionMedicamento,
        EM.Dosis,
        EM.Frecuencia,
        EM.FechaInicio,
        EM.FechaFin,
        EM.Observaciones
    FROM dbo.Estudiante_Medicamento EM
    JOIN dbo.Estudantes E ON EM.IdEstudiante = E.IdEstudiante
    JOIN dbo.Medicamentos M ON EM.IdMedicamento = M.IdMedicamento
    ORDER BY NombreEstudiante;
END