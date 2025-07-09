CREATE   PROCEDURE [Discapacidad].SP_ObtenerTodasDiscapacidades
AS
BEGIN
    SELECT D.IdDiscapacidad, D.Nombre, D.Descripcion,
           C.IdCategoria, C.Nombre AS Categoria, C.Descripcion AS CategoriaDescripcion
    FROM Discapacidades D
    JOIN CategoriasDiscapacidad C ON D.IdCategoria = C.IdCategoria;
END;