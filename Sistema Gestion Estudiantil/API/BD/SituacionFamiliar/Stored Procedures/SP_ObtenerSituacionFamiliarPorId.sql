
CREATE PROCEDURE SituacionFamiliar.SP_ObtenerSituacionFamiliarPorId
    @IdSituacion UNIQUEIDENTIFIER
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
    WHERE SF.IdSituacion = @IdSituacion;
END