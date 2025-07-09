
CREATE PROCEDURE [EstudianteAlergia].[SP_ObtenerAlergiasPorEstudiantes]
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
                EA.IdAlergia,
                A.Nombre AS NombreAlergia,
                A.Descripcion AS DescripcionAlergia,
                C.Nombre AS Categoria,
                C.Descripcion AS CategoriaDescripcion,
                EA.FechaDeteccion,
                EA.Gravedad,
                EA.Observaciones
            FROM Estudiante_Alergia EA
            JOIN Alergias A ON EA.IdAlergia = A.IdAlergia
            JOIN CategoriasAlergia C ON A.IdCategoria = C.IdCategoria
            WHERE EA.IdEstudiante = E.IdEstudiante
            FOR JSON PATH
        ) AS Alergias
    FROM Estudiantes E
    ORDER BY NombreCompleto;
END