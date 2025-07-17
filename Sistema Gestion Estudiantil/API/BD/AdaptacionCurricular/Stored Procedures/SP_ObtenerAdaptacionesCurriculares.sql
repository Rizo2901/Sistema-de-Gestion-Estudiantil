CREATE PROCEDURE AdaptacionCurricular.SP_ObtenerAdaptacionesCurriculares
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        AC.IdAdaptacion,
        AC.IdEstudiante,
        E.Cedula,
        CONCAT(E.Nombre, ' ', E.Apellido1, ' ', E.Apellido2) AS NombreEstudiante,
        AC.Tipo,
        AC.AreaAfectada,
        AC.FechaInicio,
        AC.FechaFin,
        AC.Observaciones,
        AC.Estado
    FROM dbo.AdaptacionesCurriculares AC
    JOIN dbo.Estudiantes E ON AC.IdEstudiante = E.IdEstudiante
    ORDER BY AC.FechaInicio DESC;
END