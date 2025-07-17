
CREATE   VIEW vw_PerfilEstudiantilCompleto AS
SELECT 
    E.IdEstudiante,
    E.Nombre,
    E.Apellido1 + ' ' + E.Apellido2 AS Apellidos,
    S.Nombre AS Seccion,

    -- Alergias
    A.Nombre AS Alergia,
    A.Descripcion AS DescripcionAlergia,
    CA.Nombre AS CategoriaAlergia,
    CA.Descripcion AS DescripcionCategoriaAlergia,

    -- Discapacidad
    D.Nombre AS Discapacidad,
    D.Descripcion AS DescripcionDiscapacidad,
    CD.Nombre AS CategoriaDiscapacidad,
    CD.Descripcion AS DescripcionCategoriaDiscapacidad,

    -- Condición especial
    CE.Observaciones AS ObservacionCondicion,
    CE.FechaDiagnostico AS FechaDiagnosticoCondicion,
    CCE.Nombre AS CategoriaCondicionEspecial,
    CCE.Descripcion AS DescripcionCategoriaCondicion,

    -- Enfermedades
    EN.Nombre AS Enfermedad,
    EN.Descripcion AS DescripcionEnfermedad,

    -- Medicamentos
    AM.Nombre AS Medicamento,
    AM.Descripcion AS DescripcionMedicamento,

    -- Adaptación curricular
    AC.Tipo AS TipoAdaptacion,

    -- Situación familiar
    SF.FechaRegistro AS FechaRegistroSituacionFamiliar,
SF.Descripcion AS DescripcionSituacionFamiliar,
CSF.Nombre AS CategoriaSituacionFamiliar,
CSF.Descripcion AS DescripcionCategoriaSituacionFamiliar,

    -- Calificación
    C.Calificacion,

    -- Asistencia
    ASIS.Fecha AS FechaAsistencia,
    ASIS.Estado AS EstadoAsistencia,

    -- Comportamiento
COM.Observacion AS ObservacionComportamiento,
COM.Fecha AS FechaAccionComportamiento,
COM.AccionTomada AS AccionTomadaComportamiento,

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

LEFT JOIN CondicionesEspeciales CE ON E.IdEstudiante = CE.IdEstudiante
LEFT JOIN CategoriasCondicionEspecial CCE ON CE.IdCategoria = CCE.IdCategoria

LEFT JOIN Estudiante_Enfermedad EE ON E.IdEstudiante = EE.IdEstudiante
LEFT JOIN Enfermedades EN ON EE.IdEnfermedad = EN.IdEnfermedad

LEFT JOIN Estudiante_Medicamento EM ON E.IdEstudiante = EM.IdEstudiante
LEFT JOIN Medicamentos AM ON EM.IdMedicamento = AM.IdMedicamento

LEFT JOIN AdaptacionesCurriculares AC ON E.IdEstudiante = AC.IdEstudiante

LEFT JOIN SituacionFamiliar SF ON E.IdEstudiante = SF.IdEstudiante
LEFT JOIN CategoriasSituacionFamiliar CSF ON SF.IdCategoria = CSF.IdCategoria

LEFT JOIN Calificaciones C ON E.IdEstudiante = C.IdEstudiante
LEFT JOIN Asistencia ASIS ON E.IdEstudiante = ASIS.IdEstudiante
LEFT JOIN Comportamiento COM ON E.IdEstudiante = COM.IdEstudiante
LEFT JOIN AtencionesMedicas ATM ON E.IdEstudiante = ATM.IdEstudiante;