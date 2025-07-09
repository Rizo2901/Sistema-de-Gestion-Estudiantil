
-- Eliminar Documento
CREATE PROCEDURE [Documento].SP_EliminarDocumento
    @IdDocumento UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Documentos WHERE IdDocumento = @IdDocumento;

    SELECT @IdDocumento AS IdDocumento;
END