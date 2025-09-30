-- VISTA SIMPLIFICADA: elimina subconsultas y usa agregaciones previas
CREATE VIEW [dbo].[vw_PerfilEstudiantilCompleto] AS
SELECT 
    E.IdEstudiante,
    E.Nombre,
    E.Apellido1 + ' ' + E.Apellido2 AS Apellidos,
    ISNULL(S.Nombre, 'Sin Asignar') AS Seccion,

    -- Alergias
    ISNULL(A.Nombre, 'Sin Asignar') AS Alergia,
    ISNULL(CA.Nombre, 'Sin Asignar') AS CategoriaAlergia,
    ISNULL(FAM_AL.CantidadAlergias, 0) AS CantidadAlergias,

    -- Discapacidad
    ISNULL(D.Nombre, 'Sin Asignar') AS Discapacidad,
    ISNULL(CD.Nombre, 'Sin Asignar') AS CategoriaDiscapacidad,
    ISNULL(FAM_DIS.CantidadDiscapacidades, 0) AS CantidadDiscapacidades,

    -- Enfermedades
    ISNULL(EN.Nombre, 'Sin Asignar') AS Enfermedad,
    ISNULL(FAM_ENF.CantidadEnfermedades, 0) AS CantidadEnfermedades,

    -- Medicamentos
    ISNULL(AM.Nombre, 'Sin Asignar') AS Medicamento,
    ISNULL(FAM_MED.CantidadMedicamentos, 0) AS CantidadMedicamentos,

    -- Familiares
    ISNULL(FAM.CantidadFamiliares, 0) AS CantidadFamiliares,
    ISNULL(FAM_PAD.CantidadPadres, 0) AS CantidadPadres,
    ISNULL(FAM_MAD.CantidadMadres, 0) AS CantidadMadres,
    CASE 
        WHEN ISNULL(FAM_PAD.CantidadPadres, 0) > 0 AND ISNULL(FAM_MAD.CantidadMadres, 0) > 0 THEN 1 ELSE 0 END AS ViveConAmbosPadres,
    CASE 
        WHEN ISNULL(FAM_PAD.CantidadPadres, 0) + ISNULL(FAM_MAD.CantidadMadres, 0) = 1 THEN 1 ELSE 0 END AS ViveConUnPadre,

    -- Situación familiar
    CAST(SF.Descripcion AS NVARCHAR(MAX)) AS SituacionFamiliar,
    ISNULL(CSF.Nombre, 'Sin Asignar') AS CategoriaSituacionFamiliar,
    SF.FechaRegistro AS FechaRegistroSituacionFamiliar,

    -- Condición especial
    CAST(CE.Observaciones AS NVARCHAR(MAX)) AS CondicionEspecial,
    ISNULL(CCE.Nombre, 'Sin Asignar') AS CategoriaCondicionEspecial,
    CE.FechaDiagnostico AS FechaDiagnosticoCondicion,

    -- Calificación
    ISNULL(C.Calificacion, 0) AS Calificacion,

    -- Asistencia
    ISNULL(ASIS_AUS.CantidadAusencias, 0) AS CantidadAusencias,
    ISNULL(ASIS_PRE.CantidadAsistencias, 0) AS CantidadAsistencias,
    ASIS.Fecha AS FechaAsistencia,
    ASIS.Estado AS EstadoAsistencia,

    -- Comportamiento
    CAST(COM.Observacion AS NVARCHAR(MAX)) AS ObservacionComportamiento,
    CAST(COM.AccionTomada AS NVARCHAR(MAX)) AS AccionTomadaComportamiento,
    COM.Fecha AS FechaAccionComportamiento,

    -- Atención médica
    ATM.Fecha AS FechaAtencionMedica,

    -- Documentos
    DOC.TipoDocumento,

    -- Adaptaciones
    ADAP.Tipo AS TipoAdaptacion,
    ADAP.FechaInicio AS FechaAdaptacion,

    -- Reuniones
    REU.Motivo AS MotivoReunion,
    REU.Fecha AS FechaReunion,

    -- Promedio
    PRO.Promedio

FROM Estudiantes E
LEFT JOIN Secciones S ON E.IdSeccion = S.IdSeccion
LEFT JOIN Estudiante_Alergia EA ON E.IdEstudiante = EA.IdEstudiante
LEFT JOIN Alergias A ON EA.IdAlergia = A.IdAlergia
LEFT JOIN CategoriasAlergia CA ON A.IdCategoria = CA.IdCategoria
LEFT JOIN Estudiante_Discapacidad ED ON E.IdEstudiante = ED.IdEstudiante
LEFT JOIN Discapacidades D ON ED.IdDiscapacidad = D.IdDiscapacidad
LEFT JOIN CategoriasDiscapacidad CD ON D.IdCategoria = CD.IdCategoria
LEFT JOIN Estudiante_Enfermedad EE ON E.IdEstudiante = EE.IdEstudiante
LEFT JOIN Enfermedades EN ON EE.IdEnfermedad = EN.IdEnfermedad
LEFT JOIN Estudiante_Medicamento EM ON E.IdEstudiante = EM.IdEstudiante
LEFT JOIN Medicamentos AM ON EM.IdMedicamento = AM.IdMedicamento
LEFT JOIN SituacionFamiliar SF ON E.IdEstudiante = SF.IdEstudiante
LEFT JOIN CategoriasSituacionFamiliar CSF ON SF.IdCategoria = CSF.IdCategoria
LEFT JOIN CondicionesEspeciales CE ON E.IdEstudiante = CE.IdEstudiante
LEFT JOIN CategoriasCondicionEspecial CCE ON CE.IdCategoria = CCE.IdCategoria
LEFT JOIN Calificaciones C ON E.IdEstudiante = C.IdEstudiante
LEFT JOIN Asistencia ASIS ON E.IdEstudiante = ASIS.IdEstudiante
LEFT JOIN Comportamiento COM ON E.IdEstudiante = COM.IdEstudiante
LEFT JOIN AtencionesMedicas ATM ON E.IdEstudiante = ATM.IdEstudiante
LEFT JOIN Documentos DOC ON E.IdEstudiante = DOC.IdEstudiante
LEFT JOIN AdaptacionesCurriculares ADAP ON E.IdEstudiante = ADAP.IdEstudiante
LEFT JOIN Reuniones REU ON E.IdEstudiante = REU.IdEstudiante
LEFT JOIN Promedios PRO ON E.IdEstudiante = PRO.IdEstudiante

-- Agregaciones previas para conteos
LEFT JOIN (
    SELECT IdEstudiante, COUNT(*) AS CantidadAlergias FROM Estudiante_Alergia GROUP BY IdEstudiante
) FAM_AL ON FAM_AL.IdEstudiante = E.IdEstudiante
LEFT JOIN (
    SELECT IdEstudiante, COUNT(*) AS CantidadDiscapacidades FROM Estudiante_Discapacidad GROUP BY IdEstudiante
) FAM_DIS ON FAM_DIS.IdEstudiante = E.IdEstudiante
LEFT JOIN (
    SELECT IdEstudiante, COUNT(*) AS CantidadEnfermedades FROM Estudiante_Enfermedad GROUP BY IdEstudiante
) FAM_ENF ON FAM_ENF.IdEstudiante = E.IdEstudiante
LEFT JOIN (
    SELECT IdEstudiante, COUNT(*) AS CantidadMedicamentos FROM Estudiante_Medicamento GROUP BY IdEstudiante
) FAM_MED ON FAM_MED.IdEstudiante = E.IdEstudiante
LEFT JOIN (
    SELECT IdEstudiante, COUNT(*) AS CantidadFamiliares FROM Familiares GROUP BY IdEstudiante
) FAM ON FAM.IdEstudiante = E.IdEstudiante
LEFT JOIN (
    SELECT IdEstudiante, COUNT(*) AS CantidadPadres FROM Familiares WHERE Parentesco = 'Padre' GROUP BY IdEstudiante
) FAM_PAD ON FAM_PAD.IdEstudiante = E.IdEstudiante
LEFT JOIN (
    SELECT IdEstudiante, COUNT(*) AS CantidadMadres FROM Familiares WHERE Parentesco = 'Madre' GROUP BY IdEstudiante
) FAM_MAD ON FAM_MAD.IdEstudiante = E.IdEstudiante
LEFT JOIN (
    SELECT IdEstudiante, COUNT(*) AS CantidadAusencias FROM Asistencia WHERE Estado = 'Ausente' GROUP BY IdEstudiante
) ASIS_AUS ON ASIS_AUS.IdEstudiante = E.IdEstudiante
LEFT JOIN (
    SELECT IdEstudiante, COUNT(*) AS CantidadAsistencias FROM Asistencia WHERE Estado = 'Presente' GROUP BY IdEstudiante
) ASIS_PRE ON ASIS_PRE.IdEstudiante = E.IdEstudiante