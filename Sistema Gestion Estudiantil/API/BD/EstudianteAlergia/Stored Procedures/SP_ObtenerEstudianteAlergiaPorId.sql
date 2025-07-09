
-- =============================
-- OBTENER POR ID
-- =============================
CREATE   PROCEDURE [EstudianteAlergia].SP_ObtenerEstudianteAlergiaPorId
    @IdEstudianteAlergia UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT EA.IdEstudianteAlergia, EA.IdEstudiante, EA.IdAlergia,
           A.Nombre, A.Descripcion,
           C.Nombre AS Categoria, C.Descripcion AS CategoriaDescripcion,
           EA.FechaDeteccion, EA.Gravedad, EA.Observaciones
    FROM Estudiante_Alergia EA
    JOIN Alergias A ON EA.IdAlergia = A.IdAlergia
    JOIN CategoriasAlergia C ON A.IdCategoria = C.IdCategoria
    WHERE EA.IdEstudianteAlergia = @IdEstudianteAlergia;
END;