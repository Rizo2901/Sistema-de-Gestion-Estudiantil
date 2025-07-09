
CREATE PROCEDURE SituacionFamiliar.SP_EditarCategoriaSituacionFamiliar
    @IdCategoria UNIQUEIDENTIFIER,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.CategoriasSituacionFamiliar
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE IdCategoria = @IdCategoria;
    SELECT @IdCategoria AS IdCategoriaEditada;
END