CREATE PROCEDURE [Estudiante_Discapacidad].[SP_ObtenerDiscapacidadesPorEstudiante]
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        E.IdEstudiante,
        E.Cedula,
        CONCAT(E.Nombre, ' ', E.Apellido1, ' ', E.Apellido2) AS NombreCompleto,
        E.GradoActual,
        E.Genero,
        (
            SELECT 
                ED.IdDiscapacidad,
                D.Nombre AS DiscapacidadNombre,
                ISNULL(C.Nombre, 'Sin categoría') AS CategoriaNombre,
                C.Descripcion AS CategoriaDescripcion
            FROM Estudiante_Discapacidad ED
            LEFT JOIN Discapacidades D ON ED.IdDiscapacidad = D.IdDiscapacidad
            LEFT JOIN CategoriasDiscapacidad C ON D.IdCategoria = C.IdCategoria
            WHERE ED.IdEstudiante = E.IdEstudiante
            FOR JSON PATH
        ) AS Discapacidades
    FROM Estudiantes E
    WHERE E.IdEstudiante = @IdEstudiante
    ORDER BY NombreCompleto;
END