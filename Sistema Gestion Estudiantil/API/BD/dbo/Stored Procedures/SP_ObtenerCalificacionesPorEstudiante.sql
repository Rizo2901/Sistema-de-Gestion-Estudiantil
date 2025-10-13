CREATE PROCEDURE [dbo].[SP_ObtenerCalificacionesPorEstudiante]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        E.IdEstudiante,
        CONCAT(E.Nombre, ' ', E.Apellido1, ' ', E.Apellido2) AS NombreCompleto,
        (
            SELECT 
                C.IdCalificacion,
                C.IdMateria,
                M.Nombre AS NombreMateria,
                C.Calificacion,
                C.Fecha,
                C.Comentario
            FROM dbo.Calificaciones C
            INNER JOIN dbo.Materias M ON M.IdMateria = C.IdMateria
            WHERE C.IdEstudiante = E.IdEstudiante
            FOR JSON PATH
        ) AS Calificaciones
    FROM dbo.Estudiantes E
    ORDER BY NombreCompleto;
END;