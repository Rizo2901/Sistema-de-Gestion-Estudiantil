
CREATE PROCEDURE SituacionFamiliar.SP_EliminarCategoriaSituacionFamiliar
    @IdCategoria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.CategoriasSituacionFamiliar
    WHERE IdCategoria = @IdCategoria;
    SELECT @IdCategoria AS IdCategoriaEliminada;
END