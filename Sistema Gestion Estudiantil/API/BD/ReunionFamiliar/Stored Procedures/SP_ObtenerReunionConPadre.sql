CREATE PROCEDURE [ReunionFamiliar].[SP_ObtenerReunionConPadre]
    @IdReunion UNIQUEIDENTIFIER
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
        Estudiantes E ON R.IdEstudiante = E.IdEstudiante
    WHERE 
        R.IdReunion = @IdReunion;
END;