
-- =============================
-- ELIMINAR CATEGORÍA DE CONDICIÓN ESPECIAL
-- =============================
CREATE   PROCEDURE [CondicionEspecial].SP_EliminarCategoriaCondicionEspecial
    @IdCategoria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM CategoriasCondicionEspecial
    WHERE IdCategoria = @IdCategoria;
END;