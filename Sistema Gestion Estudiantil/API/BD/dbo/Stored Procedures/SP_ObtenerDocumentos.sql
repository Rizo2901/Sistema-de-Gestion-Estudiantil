CREATE PROCEDURE [dbo].[SP_ObtenerDocumentos]
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
    FROM Documentos;
END;