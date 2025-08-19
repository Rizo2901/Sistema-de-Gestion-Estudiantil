
CREATE PROCEDURE [dbo].[SP_ObtenerFamiliarPorId]
    @IdFamiliar UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        f.IdFamiliar,
        f.IdEstudiante,
        f.Nombre,
        f.Apellido1,
        f.Apellido2,
        f.Cedula,
        f.Direccion,
        f.Parentesco,
        f.TelefonoCelular,
        f.CorreoElectronico,
        f.Ocupacion,
        f.EstadoCivil,
        f.CantidadHijos,
        f.IdUsuario,
        f.ViveConEstudiante,
        f.EsResponsableLegal,
        e.Nombre AS NombreEstudiante,
        e.Apellido1 AS Apellido1Estudiante,
        e.Apellido2 AS Apellido2Estudiante,
        e.Cedula AS CedulaEstudiante,
        e.GradoActual AS GradoEstudiante,
        e.IdSeccion AS SeccionEstudiante,
        s.Nombre AS NombreSeccion -- ✅ nuevo campo
    FROM Familiares f
    LEFT JOIN Estudiantes e ON f.IdEstudiante = e.IdEstudiante
    LEFT JOIN Secciones s ON e.IdSeccion = s.IdSeccion
    WHERE f.IdFamiliar = @IdFamiliar;

    SELECT 
        r.IdReunion,
        r.IdEstudiante,
        r.Fecha,
        r.Motivo
    FROM Reuniones r
    INNER JOIN Familiares f ON f.IdEstudiante = r.IdEstudiante
    WHERE f.IdFamiliar = @IdFamiliar
    ORDER BY r.Fecha DESC;
END