
CREATE PROCEDURE [Horario].[Sp_ObtenerHorarios]
AS
BEGIN
    SET NOCOUNT ON;

    -- Selección directa sin conversiones
    SELECT  
        h.IdHorario,
        h.IdSeccion,
        s.Nombre AS NombreSeccion,
        h.Dia,
        h.HoraInicio,  -- Tipo TIME
        h.HoraFin,     -- Tipo TIME
        h.IdMateria,
        m.Nombre AS NombreMateria,
        h.IdProfesor,
        CONCAT(p.Nombre, ' ', p.Apellido1) AS NombreProfesor,
        h.Aula
    FROM dbo.Horarios h
    INNER JOIN dbo.Secciones s ON s.IdSeccion = h.IdSeccion
    INNER JOIN dbo.Materias m ON m.IdMateria = h.IdMateria
    INNER JOIN dbo.Profesores p ON p.IdProfesor = h.IdProfesor
    -- Orden institucional por sección, día y hora
    ORDER BY s.Nombre,
             CASE h.Dia
                WHEN 'Lunes' THEN 1
                WHEN 'Martes' THEN 2
                WHEN 'Miércoles' THEN 3
                WHEN 'Jueves' THEN 4
                WHEN 'Viernes' THEN 5
                WHEN 'Sábado' THEN 6
                WHEN 'Domingo' THEN 7
                ELSE 8
             END,
             h.HoraInicio;
END