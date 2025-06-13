CREATE PROCEDURE [dbo].[SP_Obtener_Documentos_Por_Estudiante]
    @IdEstudiante UNIQUEIDENTIFIER
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
    FROM 
        Documentos
    WHERE 
        IdEstudiante = @IdEstudiante;
END;