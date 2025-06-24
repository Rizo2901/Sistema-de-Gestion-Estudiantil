CREATE PROCEDURE SP_ObtenerEstudiantesConDocumentos
AS
BEGIN
    SELECT 
        e.IdEstudiante,
        e.Nombre,
        e.Apellido1,
        e.Apellido2,
        COALESCE(
            (
                SELECT 
                    d.IdDocumento, 
                    d.TipoDocumento, 
                    d.Descripcion,
					d.TipoArchivo
                FROM Documentos d
                WHERE d.IdEstudiante = e.IdEstudiante
                ORDER BY d.IdDocumento
                FOR JSON PATH
            ),
            '[]'
        ) AS Documentos
    FROM Estudiantes e;
END;