CREATE PROCEDURE [EstudianteAlergia].[SP_ObtenerTodasLasAlergiasEstudiantes]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        EA.IdEstudianteAlergia,
        EA.IdEstudiante,
        E.Cedula,
        CONCAT(E.Nombre, ' ', E.Apellido1, ' ', E.Apellido2) AS NombreCompleto,
        E.GradoActual,
        E.Genero,
        EA.IdAlergia,
        A.Nombre AS NombreAlergia,
        A.Descripcion AS DescripcionAlergia,
        C.Nombre AS Categoria,
        C.Descripcion AS CategoriaDescripcion,
        EA.FechaDeteccion,
        EA.Gravedad,
        EA.Observaciones
    FROM Estudiante_Alergia EA
    JOIN Estudiantes E ON EA.IdEstudiante = E.IdEstudiante
    JOIN Alergias A ON EA.IdAlergia = A.IdAlergia
    JOIN CategoriasAlergia C ON A.IdCategoria = C.IdCategoria
    ORDER BY NombreCompleto;
END