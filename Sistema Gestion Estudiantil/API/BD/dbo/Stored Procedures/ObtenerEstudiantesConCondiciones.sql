CREATE PROCEDURE [dbo].[ObtenerEstudiantesConCondiciones]
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
                    c.IdCondicion, 
                    c.NombreCondicion, 
                    c.TipoCondicion
                FROM CondicionesEspeciales c
                WHERE c.IdEstudiante = e.IdEstudiante
                ORDER BY c.IdCondicion
                FOR JSON PATH
            ),
            '[]'
        ) AS Condiciones
    FROM Estudiantes e;
END;