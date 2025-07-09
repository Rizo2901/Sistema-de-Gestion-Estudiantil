CREATE   PROCEDURE [Discapacidad].SP_ObtenerDiscapacidadPorId
    @IdDiscapacidad UNIQUEIDENTIFIER
AS
BEGIN
    SELECT D.IdDiscapacidad, D.Nombre, D.Descripcion,
           C.IdCategoria, C.Nombre AS Categoria, C.Descripcion AS CategoriaDescripcion
    FROM Discapacidades D
    JOIN CategoriasDiscapacidad C ON D.IdCategoria = C.IdCategoria
    WHERE D.IdDiscapacidad = @IdDiscapacidad;
END;