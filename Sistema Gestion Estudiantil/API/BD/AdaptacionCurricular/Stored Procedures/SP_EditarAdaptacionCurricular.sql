
CREATE PROCEDURE AdaptacionCurricular.SP_EditarAdaptacionCurricular
    @IdAdaptacion UNIQUEIDENTIFIER,
    @IdEstudiante UNIQUEIDENTIFIER,
    @Tipo VARCHAR(100),
    @AreaAfectada VARCHAR(100),
    @FechaInicio DATE,
    @FechaFin DATE,
    @Observaciones TEXT,
    @Estado VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.AdaptacionesCurriculares
    SET 
        IdEstudiante = @IdEstudiante,
        Tipo = @Tipo,
        AreaAfectada = @AreaAfectada,
        FechaInicio = @FechaInicio,
        FechaFin = @FechaFin,
        Observaciones = @Observaciones,
        Estado = @Estado
    WHERE IdAdaptacion = @IdAdaptacion;

    SELECT @IdAdaptacion AS IdAdaptacionEditada;
END