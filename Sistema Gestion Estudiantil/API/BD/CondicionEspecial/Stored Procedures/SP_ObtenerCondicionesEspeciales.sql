

CREATE PROCEDURE [CondicionEspecial].[SP_ObtenerCondicionesEspeciales]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        e.IdEstudiante,
        e.Cedula,
        e.Nombre,
        e.Apellido1,
        e.Apellido2,

        c.IdCondicion,
        c.IdCategoria,
        cat.Nombre AS NombreCategoria,
        cat.Descripcion AS DescripcionCategoria,
        c.FechaDiagnostico,
        c.Observaciones
    FROM Estudiantes e
    LEFT JOIN CondicionesEspeciales c ON e.IdEstudiante = c.IdEstudiante
    LEFT JOIN CategoriasCondicionEspecial cat ON c.IdCategoria = cat.IdCategoria
    ORDER BY e.Apellido1, e.Apellido2;
END;