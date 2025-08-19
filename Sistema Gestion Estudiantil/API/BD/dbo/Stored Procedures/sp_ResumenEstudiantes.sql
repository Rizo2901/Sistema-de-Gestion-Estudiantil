CREATE PROCEDURE [dbo].[sp_ResumenEstudiantes]

AS
BEGIN
    SELECT 
        e.IdEstudiante,
        e.Cedula,
        e.Nombre,
        e.Apellido1,
        e.Apellido2,
        e.FechaNacimiento,
        e.Genero,
        e.GradoActual,
        e.Repitiendo,
        e.HistorialAcademico,
        e.Fotografia,
        e.Direccion,
        e.TelefonoCelular,
        e.CorreoElectronico,
        e.Estado,
        e.IdSeccion,
        e.IdUsuario,

        ISNULL(docs.Documentos, 0) AS Documentos,
        ISNULL(alergias.Alergias, 0) AS Alergias,
        ISNULL(discapacidades.Discapacidades, 0) AS Discapacidades,
        ISNULL(enfermedades.Enfermedades, 0) AS Enfermedades,
        ISNULL(meds.Medicamentos, 0) AS Medicamentos,
        ISNULL(condiciones.CondicionesEspeciales, 0) AS CondicionesEspeciales,
        ISNULL(situaciones.SituacionesFamiliares, 0) AS SituacionesFamiliares,
        ISNULL(adaptacionesCurriculares.AdaptacionesCurriculares, 0) AS AdaptacionesCurriculares,
        ISNULL(atencionesMedicas.AtencionesMedicas, 0) AS AtencionesMedicas
    FROM Estudiantes e
    LEFT JOIN (
        SELECT IdEstudiante, COUNT(*) AS Documentos
        FROM Documentos
        GROUP BY IdEstudiante
    ) docs ON e.IdEstudiante = docs.IdEstudiante
    LEFT JOIN (
        SELECT IdEstudiante, COUNT(*) AS Alergias
        FROM Estudiante_Alergia
        GROUP BY IdEstudiante
    ) alergias ON e.IdEstudiante = alergias.IdEstudiante
    LEFT JOIN (
        SELECT IdEstudiante, COUNT(*) AS Discapacidades
        FROM Estudiante_Discapacidad
        GROUP BY IdEstudiante
    ) discapacidades ON e.IdEstudiante = discapacidades.IdEstudiante
    LEFT JOIN (
        SELECT IdEstudiante, COUNT(*) AS Enfermedades
        FROM Estudiante_Enfermedad
        GROUP BY IdEstudiante
    ) enfermedades ON e.IdEstudiante = enfermedades.IdEstudiante
    LEFT JOIN (
        SELECT IdEstudiante, COUNT(*) AS Medicamentos
        FROM Estudiante_Medicamento
        GROUP BY IdEstudiante
    ) meds ON e.IdEstudiante = meds.IdEstudiante
    LEFT JOIN (
        SELECT IdEstudiante, COUNT(*) AS CondicionesEspeciales
        FROM CondicionesEspeciales
        GROUP BY IdEstudiante
    ) condiciones ON e.IdEstudiante = condiciones.IdEstudiante
    LEFT JOIN (
        SELECT IdEstudiante, COUNT(*) AS SituacionesFamiliares
        FROM SituacionFamiliar
        GROUP BY IdEstudiante
    ) situaciones ON e.IdEstudiante = situaciones.IdEstudiante
    LEFT JOIN (
        SELECT IdEstudiante, COUNT(*) AS AdaptacionesCurriculares
        FROM AdaptacionesCurriculares
        GROUP BY IdEstudiante
    ) adaptacionesCurriculares ON e.IdEstudiante = adaptacionesCurriculares.IdEstudiante
    LEFT JOIN (
        SELECT IdEstudiante, COUNT(*) AS AtencionesMedicas
        FROM AtencionesMedicas
        GROUP BY IdEstudiante
    ) atencionesMedicas ON e.IdEstudiante = atencionesMedicas.IdEstudiante
END