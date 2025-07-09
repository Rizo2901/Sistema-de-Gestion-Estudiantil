
-- =============================
-- EDITAR CATEGORÍA DE CONDICIÓN ESPECIAL
-- =============================
CREATE   PROCEDURE [CondicionEspecial].SP_EditarCategoriaCondicionEspecial
    @IdCategoria UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CategoriasCondicionEspecial
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE IdCategoria = @IdCategoria;
END;