
-- =============================
-- CREAR CATEGORÍA DE DISCAPACIDAD
-- =============================
CREATE   PROCEDURE [CategoriaDiscapacidad].SP_CrearCategoriaDiscapacidad
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO CategoriasDiscapacidad (IdCategoria, Nombre, Descripcion)
    VALUES (@NuevoId, @Nombre, @Descripcion);

    SELECT @NuevoId AS IdCategoria;
END;