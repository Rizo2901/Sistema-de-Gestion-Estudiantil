
-- =============================
-- CREAR CATEGORÍA DE ALERGIA
-- =============================
CREATE   PROCEDURE [EstudianteAlergia].SP_CrearCategoriaAlergia
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO CategoriasAlergia (IdCategoria, Nombre, Descripcion)
    VALUES (@NuevoId, @Nombre, @Descripcion);

    SELECT @NuevoId AS IdCategoria;
END;