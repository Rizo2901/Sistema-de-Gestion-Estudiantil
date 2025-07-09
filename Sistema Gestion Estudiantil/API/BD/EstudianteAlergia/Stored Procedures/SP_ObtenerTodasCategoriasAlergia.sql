
-- =============================
-- OBTENER TODAS LAS CATEGORÍAS
-- =============================
CREATE   PROCEDURE [EstudianteAlergia].SP_ObtenerTodasCategoriasAlergia
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdCategoria, Nombre, Descripcion
    FROM CategoriasAlergia
    ORDER BY Nombre;
END;