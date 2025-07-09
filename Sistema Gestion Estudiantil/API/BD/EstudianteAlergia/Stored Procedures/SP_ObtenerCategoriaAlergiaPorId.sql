
-- =============================
-- OBTENER CATEGORÍA POR ID
-- =============================
CREATE   PROCEDURE [EstudianteAlergia].SP_ObtenerCategoriaAlergiaPorId
    @IdCategoria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdCategoria, Nombre, Descripcion
    FROM CategoriasAlergia
    WHERE IdCategoria = @IdCategoria;
END;