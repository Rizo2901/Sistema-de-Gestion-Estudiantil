CREATE PROCEDURE [Seccion].[SP_ObtenerSeccionPorId]
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SELECT IdSeccion, Nombre
    FROM Secciones
    WHERE IdSeccion = @IdSeccion;
END;