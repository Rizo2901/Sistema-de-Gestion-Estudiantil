CREATE PROCEDURE SituacionFamiliar.SP_CrearCategoriaSituacionFamiliar
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IdCategoria UNIQUEIDENTIFIER = NEWID();
    INSERT INTO dbo.CategoriasSituacionFamiliar (IdCategoria, Nombre, Descripcion)
    VALUES (@IdCategoria, @Nombre, @Descripcion);
    SELECT @IdCategoria AS IdCategoriaCreada;
END