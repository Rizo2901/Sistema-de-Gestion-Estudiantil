CREATE PROCEDURE [EstudianteAlergia].[SP_ObtenerAlergiasPorEstudiante]
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        EA.IdEstudianteAlergia,
        EA.IdEstudiante,
        EA.IdAlergia,
        EA.FechaDeteccion AS FechaDiagnostico,
        EA.Gravedad,
        EA.Observaciones,

        A.Nombre AS NombreAlergia,
        A.Descripcion AS DescripcionAlergia,

        E.Nombre AS NombreEstudiante,
        E.Apellido1 AS Apellido1Estudiante,
        E.Apellido2 AS Apellido2Estudiante,
        E.Cedula,
        E.GradoActual,
        E.Genero,

        C.Nombre AS Categoria,
        C.Descripcion AS CategoriaDescripcion
    FROM Estudiante_Alergia EA
    JOIN Estudiantes E ON EA.IdEstudiante = E.IdEstudiante
    JOIN Alergias A ON EA.IdAlergia = A.IdAlergia
    JOIN CategoriasAlergia C ON A.IdCategoria = C.IdCategoria
    WHERE EA.IdEstudiante = @IdEstudiante;
END