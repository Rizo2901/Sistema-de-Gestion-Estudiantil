
-- =============================
-- ELIMINAR CATEGORÍA DE DISCAPACIDAD
-- =============================
CREATE   PROCEDURE [CategoriaDiscapacidad].SP_EliminarCategoriaDiscapacidad
    @IdCategoria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM CategoriasDiscapacidad
    WHERE IdCategoria = @IdCategoria;
END;