CREATE PROCEDURE [CondicionEspecial].[SP_ObtenerCondicionesEspeciales]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CE.IdCondicion,
        CE.FechaDiagnostico,
        CE.Observaciones,
        
        -- Datos del estudiante
        E.IdEstudiante,
        E.Nombre,
        E.Apellido1,
        E.Apellido2,

        -- Datos de la categoría
        C.IdCategoria,
        C.Nombre,
        C.Descripcion
    FROM CondicionesEspeciales CE
    INNER JOIN Estudiantes E ON CE.IdEstudiante = E.IdEstudiante
    INNER JOIN CategoriasCondicionEspecial C ON CE.IdCategoria = C.IdCategoria
    ORDER BY E.Apellido1, E.Apellido2, E.Nombre;
END;