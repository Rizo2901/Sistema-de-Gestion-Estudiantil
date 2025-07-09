CREATE   PROCEDURE [EstudianteAlergia].SP_ObtenerAlergiaPorId
    @IdAlergia UNIQUEIDENTIFIER
AS
BEGIN
    SELECT A.IdAlergia, A.Nombre, A.Descripcion,
           C.IdCategoria, C.Nombre AS Categoria, C.Descripcion AS CategoriaDescripcion
    FROM Alergias A
    JOIN CategoriasAlergia C ON A.IdCategoria = C.IdCategoria
    WHERE A.IdAlergia = @IdAlergia;
END;