
CREATE PROCEDURE [dbo].[SP_ObtenerCalificaciones]
AS
BEGIN
    SELECT
        e.IdEstudiante,
        e.Cedula,
        e.Nombre,
        e.Apellido1,
        e.Apellido2,
        c.IdCalificacion,
        c.IdMateria,
        m.Nombre AS NombreMateria,
        c.Calificacion,
        c.Comentario,
        c.Fecha
    FROM Estudiantes e
    LEFT JOIN Calificaciones c ON e.IdEstudiante = c.IdEstudiante
    LEFT JOIN Materias m ON c.IdMateria = m.IdMateria
END