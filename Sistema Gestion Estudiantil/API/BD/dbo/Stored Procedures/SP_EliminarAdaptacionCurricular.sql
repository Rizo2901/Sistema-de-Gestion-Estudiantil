
-- =============================
-- ELIMINAR
-- =============================
CREATE   PROCEDURE SP_EliminarAdaptacionCurricular
    @IdAdaptacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM AdaptacionesCurriculares
    WHERE IdAdaptacion = @IdAdaptacion;
END;