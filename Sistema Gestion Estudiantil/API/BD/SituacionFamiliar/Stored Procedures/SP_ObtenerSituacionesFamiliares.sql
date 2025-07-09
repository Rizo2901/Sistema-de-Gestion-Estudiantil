
CREATE PROCEDURE SituacionFamiliar.SP_ObtenerSituacionesFamiliares
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        SF.IdSituacion,
        SF.IdEstudiante,
        SF.IdCategoria,
        CSF.Nombre AS NombreCategoria,
        CSF.Descripcion AS DescripcionCategoria,
        SF.Descripcion,
        SF.FechaRegistro
    FROM dbo.SituacionFamiliar SF
    JOIN dbo.CategoriasSituacionFamiliar CSF ON SF.IdCategoria = CSF.IdCategoria
    ORDER BY SF.FechaRegistro DESC;
END