CREATE PROCEDURE [CondicionEspecial].[SP_ObtenerCondicionEspecialPorId]
    @IdCondicion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        cce.IdCategoria,
        cce.Nombre AS NombreCategoria,
        cce.Descripcion AS DescripcionCategoria
    FROM CondicionesEspeciales ce
    INNER JOIN CategoriasCondicionEspecial cce 
        ON ce.IdCategoria = cce.IdCategoria
    WHERE ce.IdCondicion = @IdCondicion;
END;