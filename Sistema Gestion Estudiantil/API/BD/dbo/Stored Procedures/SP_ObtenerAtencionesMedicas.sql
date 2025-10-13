CREATE PROCEDURE [dbo].[SP_ObtenerAtencionesMedicas]
AS
BEGIN
    SELECT
        e.IdEstudiante,
        e.Cedula,
        e.Nombre,
        e.Apellido1,
        e.Apellido2,
        a.IdAtencionMedica,
		a.Diagnostico,
		a.Fecha,
		a.Tratamiento
    FROM Estudiantes e
    LEFT JOIN AtencionesMedicas a ON e.IdEstudiante = a.IdEstudiante
END