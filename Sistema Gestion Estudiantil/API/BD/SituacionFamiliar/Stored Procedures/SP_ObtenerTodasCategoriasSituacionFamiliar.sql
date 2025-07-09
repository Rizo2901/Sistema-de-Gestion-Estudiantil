
CREATE PROCEDURE SituacionFamiliar.SP_ObtenerTodasCategoriasSituacionFamiliar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdCategoria, Nombre, Descripcion
    FROM dbo.CategoriasSituacionFamiliar
    ORDER BY Nombre;
END