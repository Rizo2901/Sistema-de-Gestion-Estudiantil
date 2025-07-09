CREATE PROCEDURE [ReunionFamiliar].[SP_ObtenerReunionesConPadre]
AS
BEGIN
    SELECT 
        R.*,
        E.Nombre,
        E.Apellido1,
        E.Apellido2
    FROM 
        Reuniones R
    INNER JOIN 
        Estudiantes E ON R.IdEstudiante = E.IdEstudiante;
END;