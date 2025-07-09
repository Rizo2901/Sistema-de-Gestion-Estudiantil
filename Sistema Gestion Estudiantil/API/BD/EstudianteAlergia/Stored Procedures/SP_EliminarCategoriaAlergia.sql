
-- =============================
-- ELIMINAR CATEGORÍA DE ALERGIA
-- =============================
CREATE   PROCEDURE [EstudianteAlergia].SP_EliminarCategoriaAlergia
    @IdCategoria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM CategoriasAlergia
    WHERE IdCategoria = @IdCategoria;
END;