CREATE PROCEDURE SituacionFamiliar.SP_ObtenerSituacionesFamiliaresPorEstudiantes
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
                SF.IdSituacion,
                SF.IdCategoria,
                CSF.Nombre AS NombreCategoria,
                CSF.Descripcion AS DescripcionCategoria,
                SF.Descripcion,
                SF.FechaRegistro
            FROM dbo.SituacionFamiliar SF
            JOIN dbo.CategoriasSituacionFamiliar CSF ON SF.IdCategoria = CSF.IdCategoria
            WHERE SF.IdEstudiante = E.IdEstudiante
            FOR JSON PATH
        ) AS Situaciones
    FROM dbo.Estudiantes E
    WHERE EXISTS (
        SELECT 1 FROM dbo.SituacionFamiliar SF WHERE SF.IdEstudiante = E.IdEstudiante
    )
    ORDER BY NombreCompleto;
END