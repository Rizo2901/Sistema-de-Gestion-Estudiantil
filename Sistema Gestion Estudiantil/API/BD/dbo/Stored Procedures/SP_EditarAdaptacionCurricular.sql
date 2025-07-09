
-- =============================
-- EDITAR
-- =============================
CREATE   PROCEDURE SP_EditarAdaptacionCurricular
    @IdAdaptacion UNIQUEIDENTIFIER,
    @Tipo VARCHAR(100),
    @AreaAfectada VARCHAR(100),
    @FechaInicio DATE,
    @FechaFin DATE,
    @Estado VARCHAR(50),
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE AdaptacionesCurriculares
    SET Tipo = @Tipo,
        AreaAfectada = @AreaAfectada,
        FechaInicio = @FechaInicio,
        FechaFin = @FechaFin,
        Estado = @Estado,
        Observaciones = @Observaciones
    WHERE IdAdaptacion = @IdAdaptacion;
END;