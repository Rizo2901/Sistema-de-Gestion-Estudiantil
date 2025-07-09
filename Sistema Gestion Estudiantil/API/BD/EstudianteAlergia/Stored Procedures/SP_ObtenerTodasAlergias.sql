CREATE   PROCEDURE [EstudianteAlergia].SP_ObtenerTodasAlergias
AS
BEGIN
    SELECT A.IdAlergia, A.Nombre, A.Descripcion,
           C.IdCategoria, C.Nombre AS Categoria, C.Descripcion AS CategoriaDescripcion
    FROM Alergias A
    JOIN CategoriasAlergia C ON A.IdCategoria = C.IdCategoria;
END;