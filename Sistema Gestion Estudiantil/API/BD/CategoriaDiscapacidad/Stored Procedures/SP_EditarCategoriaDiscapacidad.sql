
-- =============================
-- EDITAR CATEGORÍA DE DISCAPACIDAD
-- =============================
CREATE   PROCEDURE [CategoriaDiscapacidad].SP_EditarCategoriaDiscapacidad
    @IdCategoria UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CategoriasDiscapacidad
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE IdCategoria = @IdCategoria;
END;