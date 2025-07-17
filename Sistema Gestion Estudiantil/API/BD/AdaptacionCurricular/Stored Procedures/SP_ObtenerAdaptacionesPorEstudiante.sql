CREATE PROCEDURE AdaptacionCurricular.SP_ObtenerAdaptacionesPorEstudiante
    @IdEstudiante UNIQUEIDENTIFIER
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
    WHERE AC.IdEstudiante = @IdEstudiante;
END