CREATE PROCEDURE AdaptacionCurricular.SP_ObtenerAdaptacionesPorEstudiantes
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
                AC.IdAdaptacion,
                AC.Tipo,
                AC.AreaAfectada,
                AC.FechaInicio,
                AC.FechaFin,
                AC.Observaciones,
                AC.Estado
            FROM dbo.AdaptacionesCurriculares AC
            WHERE AC.IdEstudiante = E.IdEstudiante
            FOR JSON PATH
        ) AS Adaptaciones
    FROM dbo.Estudiantes E
    WHERE EXISTS (
        SELECT 1 FROM dbo.AdaptacionesCurriculares AC WHERE AC.IdEstudiante = E.IdEstudiante
    )
    ORDER BY NombreCompleto;
END