CREATE PROCEDURE AdaptacionCurricular.SP_EliminarAdaptacionCurricular
    @IdAdaptacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.AdaptacionesCurriculares
    WHERE IdAdaptacion = @IdAdaptacion;

    SELECT @IdAdaptacion AS IdAdaptacionEliminada;
END