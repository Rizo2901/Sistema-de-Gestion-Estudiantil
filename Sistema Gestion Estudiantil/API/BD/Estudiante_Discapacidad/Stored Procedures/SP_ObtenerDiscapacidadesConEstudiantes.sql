CREATE   PROCEDURE Estudiante_Discapacidad.SP_ObtenerDiscapacidadesConEstudiantes
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
        ED.FechaDiagnostico,
        ED.GradoAfectacion,
        ED.Observaciones,
        D.IdDiscapacidad,
        D.Nombre AS Discapacidad,
        D.Descripcion AS DescripcionDiscapacidad,
        C.Nombre AS Categoria,
        C.Descripcion AS DescripcionCategoria
    FROM Estudiante_Discapacidad ED
    INNER JOIN Estudiantes E ON E.IdEstudiante = ED.IdEstudiante
    INNER JOIN Discapacidades D ON D.IdDiscapacidad = ED.IdDiscapacidad
    INNER JOIN CategoriasDiscapacidad C ON C.IdCategoria = D.IdCategoria
    ORDER BY E.Nombre, D.Nombre;
END;