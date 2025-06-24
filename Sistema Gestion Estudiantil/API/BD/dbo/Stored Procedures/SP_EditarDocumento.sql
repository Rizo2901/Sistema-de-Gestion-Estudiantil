CREATE PROCEDURE [dbo].[SP_EditarDocumento]
    @IdDocumento UNIQUEIDENTIFIER,
    @TipoDocumento VARCHAR(50),
    @Descripcion TEXT,
    @Archivo VARCHAR(MAX),
    @TipoArchivo VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Documentos
    SET
        TipoDocumento = @TipoDocumento,
        Descripcion = @Descripcion,
        Archivo = @Archivo,
        TipoArchivo = @TipoArchivo
    WHERE
        IdDocumento = @IdDocumento;
END;