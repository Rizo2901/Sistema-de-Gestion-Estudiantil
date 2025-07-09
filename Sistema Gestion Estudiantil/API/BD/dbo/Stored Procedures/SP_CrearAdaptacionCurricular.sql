
-- =============================
-- CREAR
-- =============================
CREATE   PROCEDURE SP_CrearAdaptacionCurricular
    @IdEstudiante UNIQUEIDENTIFIER,
    @Tipo VARCHAR(100),
    @AreaAfectada VARCHAR(100),
    @FechaInicio DATE,
    @FechaFin DATE,
    @Estado VARCHAR(50),
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO AdaptacionesCurriculares (
        IdAdaptacion, IdEstudiante, Tipo, AreaAfectada,
        FechaInicio, FechaFin, Estado, Observaciones
    )
    VALUES (
        @NuevoId, @IdEstudiante, @Tipo, @AreaAfectada,
        @FechaInicio, @FechaFin, @Estado, @Observaciones
    );

    SELECT @NuevoId AS IdAdaptacion;
END;