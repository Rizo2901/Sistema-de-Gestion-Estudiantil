
CREATE PROCEDURE [Documento].[SP_ObtenerDocumentos]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  
        d.IdDocumento,
        d.IdEstudiante,
        d.TipoDocumento,
        d.Descripcion,
        d.Archivo,
        d.TipoArchivo,
        e.IdEstudiante,
        e.Cedula,
        e.Nombre,
        e.Apellido1,
        e.Apellido2
    FROM Estudiantes e
    LEFT JOIN Documentos d ON d.IdEstudiante = e.IdEstudiante;
END;