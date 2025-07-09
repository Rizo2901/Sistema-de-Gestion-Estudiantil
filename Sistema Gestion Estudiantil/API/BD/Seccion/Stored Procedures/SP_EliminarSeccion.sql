CREATE PROCEDURE [Seccion].[SP_EliminarSeccion]
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Secciones
    WHERE IdSeccion = @IdSeccion;
END;