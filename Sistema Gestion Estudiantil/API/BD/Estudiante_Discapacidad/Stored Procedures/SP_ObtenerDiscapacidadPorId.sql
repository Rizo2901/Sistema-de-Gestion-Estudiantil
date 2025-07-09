
CREATE PROCEDURE [Estudiante_Discapacidad].[SP_ObtenerDiscapacidadPorId]
    @IdEstudianteDiscapacidad UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        E.IdEstudiante,
        E.Cedula,
        E.Nombre + ' ' + E.Apellido1 + ' ' + E.Apellido2 AS NombreCompleto,
        E.GradoActual,
        E.Genero,
        ED.IdEstudianteDiscapacidad,
        ED.IdDiscapacidad,
        D.Nombre AS NombreDiscapacidad,
        D.Descripcion AS DescripcionDiscapacidad,
        C.Nombre AS Categoria,
        C.Descripcion AS CategoriaDescripcion,
        ED.FechaDiagnostico,
        ED.GradoAfectacion,
        ED.Observaciones
    FROM Estudiante_Discapacidad ED
    JOIN Estudiantes E ON ED.IdEstudiante = E.IdEstudiante
    JOIN Discapacidades D ON ED.IdDiscapacidad = D.IdDiscapacidad
    LEFT JOIN CategoriasDiscapacidad C ON D.IdCategoria = C.IdCategoria
    WHERE ED.IdEstudianteDiscapacidad = @IdEstudianteDiscapacidad;
END;