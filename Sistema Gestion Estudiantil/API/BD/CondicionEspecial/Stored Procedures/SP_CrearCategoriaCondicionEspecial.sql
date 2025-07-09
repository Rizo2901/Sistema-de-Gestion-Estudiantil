
-- =============================
-- CREAR CATEGORÍA DE CONDICIÓN ESPECIAL
-- =============================
CREATE   PROCEDURE [CondicionEspecial].SP_CrearCategoriaCondicionEspecial
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO CategoriasCondicionEspecial (IdCategoria, Nombre, Descripcion)
    VALUES (@NuevoId, @Nombre, @Descripcion);

    SELECT @NuevoId AS IdCategoria;
END;