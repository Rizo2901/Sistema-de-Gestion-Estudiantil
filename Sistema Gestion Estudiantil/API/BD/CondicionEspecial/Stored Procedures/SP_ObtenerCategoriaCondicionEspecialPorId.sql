
-- =============================
-- OBTENER CATEGORÍA POR ID
-- =============================
CREATE   PROCEDURE [CondicionEspecial].SP_ObtenerCategoriaCondicionEspecialPorId
    @IdCategoria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdCategoria, Nombre, Descripcion
    FROM CategoriasCondicionEspecial
    WHERE IdCategoria = @IdCategoria;
END;