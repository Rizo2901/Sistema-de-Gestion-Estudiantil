CREATE PROCEDURE AdaptacionCurricular.SP_CrearAdaptacionCurricular
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

    DECLARE @IdAdaptacion UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.AdaptacionesCurriculares (
        IdAdaptacion, IdEstudiante, Tipo, AreaAfectada, FechaInicio, FechaFin, Observaciones, Estado
    )
    VALUES (
        @IdAdaptacion, @IdEstudiante, @Tipo, @AreaAfectada, @FechaInicio, @FechaFin, @Observaciones, @Estado
    );

    SELECT @IdAdaptacion AS IdAdaptacionCreada;
END