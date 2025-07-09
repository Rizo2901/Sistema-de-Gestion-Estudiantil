
-- =============================
-- EDITAR CATEGORÍA DE ALERGIA
-- =============================
CREATE   PROCEDURE [EstudianteAlergia].SP_EditarCategoriaAlergia
    @IdCategoria UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CategoriasAlergia
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE IdCategoria = @IdCategoria;
END;