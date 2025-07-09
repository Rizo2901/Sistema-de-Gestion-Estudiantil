CREATE PROCEDURE [Documento].[SP_ObtenerDocumentoPorId]
    @IdDocumento UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdDocumento, 
        IdEstudiante, 
        TipoDocumento, 
        Descripcion, 
        Archivo,
        TipoArchivo
    FROM Documentos
    WHERE IdDocumento = @IdDocumento;
END;