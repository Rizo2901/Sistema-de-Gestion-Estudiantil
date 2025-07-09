
-- =============================
-- OBTENER TODAS LAS CATEGORÍAS
-- =============================
CREATE   PROCEDURE [CategoriaDiscapacidad].SP_ObtenerTodasCategoriasDiscapacidad
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdCategoria, Nombre, Descripcion
    FROM CategoriasDiscapacidad
    ORDER BY Nombre;
END;