
CREATE PROCEDURE SituacionFamiliar.SP_ObtenerCategoriaSituacionFamiliarPorId
    @IdCategoria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdCategoria, Nombre, Descripcion
    FROM dbo.CategoriasSituacionFamiliar
    WHERE IdCategoria = @IdCategoria;
END