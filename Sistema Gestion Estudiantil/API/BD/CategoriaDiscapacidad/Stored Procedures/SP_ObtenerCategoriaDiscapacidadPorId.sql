
-- =============================
-- OBTENER CATEGORÍA POR ID
-- =============================
CREATE   PROCEDURE [CategoriaDiscapacidad].SP_ObtenerCategoriaDiscapacidadPorId
    @IdCategoria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdCategoria, Nombre, Descripcion
    FROM CategoriasDiscapacidad
    WHERE IdCategoria = @IdCategoria;
END;