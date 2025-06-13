CREATE PROCEDURE SP_ObtenerDocumentosPorEstudiante
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdDocumento, IdEstudiante, TipoDocumento, Descripcion, Archivo
    FROM Documentos
    WHERE IdEstudiante = @IdEstudiante;
END