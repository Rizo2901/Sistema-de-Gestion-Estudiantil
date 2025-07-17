CREATE VIEW [dbo].[vw_PerfilEstudiantilCompleto] AS
SELECT 
    E.IdEstudiante,
    E.Nombre,
    E.Apellido1 + ' ' + E.Apellido2 AS Apellidos,
    ISNULL(S.Nombre, 'Sin Asignar') AS Seccion,

    -- Alergias
    ISNULL(A.Nombre, 'Sin Asignar') AS Alergia,
    ISNULL(CA.Nombre, 'Sin Asignar') AS CategoriaAlergia,
    (SELECT COUNT(*) FROM Estudiante_Alergia EA2 WHERE EA2.IdEstudiante = E.IdEstudiante) AS CantidadAlergias,

    -- Discapacidad
    ISNULL(D.Nombre, 'Sin Asignar') AS Discapacidad,
    ISNULL(CD.Nombre, 'Sin Asignar') AS CategoriaDiscapacidad,
    (SELECT COUNT(*) FROM Estudiante_Discapacidad ED2 WHERE ED2.IdEstudiante = E.IdEstudiante) AS CantidadDiscapacidades,

    -- Enfermedades
    ISNULL(EN.Nombre, 'Sin Asignar') AS Enfermedad,
    (SELECT COUNT(*) FROM Estudiante_Enfermedad EE2 WHERE EE2.IdEstudiante = E.IdEstudiante) AS CantidadEnfermedades,

    -- Medicamentos
    ISNULL(AM.Nombre, 'Sin Asignar') AS Medicamento,
    (SELECT COUNT(*) FROM Estudiante_Medicamento EM2 WHERE EM2.IdEstudiante = E.IdEstudiante) AS CantidadMedicamentos,

    -- Familiares
    (SELECT COUNT(*) FROM Familiares F WHERE F.IdEstudiante = E.IdEstudiante) AS CantidadFamiliares,
    (SELECT COUNT(*) FROM Familiares F WHERE F.IdEstudiante = E.IdEstudiante AND F.Parentesco = 'Padre') AS CantidadPadres,
    (SELECT COUNT(*) FROM Familiares F WHERE F.IdEstudiante = E.IdEstudiante AND F.Parentesco = 'Madre') AS CantidadMadres,
    CASE 
        WHEN (SELECT COUNT(*) FROM Familiares F WHERE F.IdEstudiante = E.IdEstudiante AND F.Parentesco = 'Padre') > 0
         AND (SELECT COUNT(*) FROM Familiares F WHERE F.IdEstudiante = E.IdEstudiante AND F.Parentesco = 'Madre') > 0
        THEN 1 ELSE 0 END AS ViveConAmbosPadres,
    CASE 
        WHEN (SELECT COUNT(*) FROM Familiares F WHERE F.IdEstudiante = E.IdEstudiante AND F.Parentesco IN ('Padre','Madre')) = 1
        THEN 1 ELSE 0 END AS ViveConUnPadre,

    -- Situación familiar
    ISNULL(SF.Descripcion, 'Sin Asignar') AS SituacionFamiliar,
    ISNULL(CSF.Nombre, 'Sin Asignar') AS CategoriaSituacionFamiliar,
    ISNULL(SF.FechaRegistro, NULL) AS FechaRegistroSituacionFamiliar,

    -- Condición especial
    ISNULL(CE.Observaciones, 'Sin Asignar') AS CondicionEspecial,
    ISNULL(CCE.Nombre, 'Sin Asignar') AS CategoriaCondicionEspecial,
    ISNULL(CE.FechaDiagnostico, NULL) AS FechaDiagnosticoCondicion,

    -- Calificación
    ISNULL(C.Calificacion, 0) AS Calificacion,

    -- Asistencia
    (SELECT COUNT(*) FROM Asistencia ASIS2 WHERE ASIS2.IdEstudiante = E.IdEstudiante AND ASIS2.Estado = 'Ausente') AS CantidadAusencias,
    (SELECT COUNT(*) FROM Asistencia ASIS2 WHERE ASIS2.IdEstudiante = E.IdEstudiante AND ASIS2.Estado = 'Presente') AS CantidadAsistencias,
    ASIS.Fecha AS FechaAsistencia,

    -- Comportamiento
    ISNULL(COM.Observacion, 'Sin Asignar') AS ObservacionComportamiento,
    ISNULL(COM.AccionTomada, 'Sin Asignar') AS AccionTomadaComportamiento,
    COM.Fecha AS FechaAccionComportamiento,

    -- Atención médica
    ATM.Fecha AS FechaAtencionMedica

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
LEFT JOIN AtencionesMedicas ATM ON E.IdEstudiante = ATM.IdEstudiante;