
-- =============================
-- OBTENER POR ESTUDIANTE
-- =============================
CREATE   PROCEDURE SP_ObtenerAdaptacionesPorEstudiante
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdAdaptacion, Tipo, AreaAfectada,
           FechaInicio, FechaFin, Estado, Observaciones
    FROM AdaptacionesCurriculares
    WHERE IdEstudiante = @IdEstudiante;
END;