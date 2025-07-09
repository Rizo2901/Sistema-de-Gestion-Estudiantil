CREATE PROCEDURE [CondicionEspecial].[SP_ObtenerCondicionesPorEstudiante]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        E.IdEstudiante,
        CONCAT(E.Nombre, ' ', E.Apellido1, ' ', E.Apellido2) AS NombreCompleto,
        (
            SELECT 
                CE.IdCondicion,
                CE.FechaDiagnostico,
                CE.Observaciones,
                CC.Nombre AS CategoriaNombre,
                CC.Descripcion AS CategoriaDescripcion
            FROM CondicionesEspeciales CE
            LEFT JOIN CategoriasCondicionEspecial CC ON CC.IdCategoria = CE.IdCategoria
            WHERE CE.IdEstudiante = E.IdEstudiante
            FOR JSON PATH
        ) AS Condiciones
    FROM Estudiantes E
    ORDER BY NombreCompleto;
END;