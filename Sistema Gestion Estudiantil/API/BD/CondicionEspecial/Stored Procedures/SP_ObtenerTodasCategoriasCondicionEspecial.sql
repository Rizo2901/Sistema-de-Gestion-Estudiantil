
-- =============================
-- OBTENER TODAS LAS CATEGORÍAS
-- =============================
CREATE   PROCEDURE [CondicionEspecial].SP_ObtenerTodasCategoriasCondicionEspecial
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdCategoria, Nombre, Descripcion
    FROM CategoriasCondicionEspecial
    ORDER BY Nombre;
END;