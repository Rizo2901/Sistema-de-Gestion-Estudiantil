CREATE PROCEDURE SP_ObtenerSeccionesProfesor
AS
BEGIN
    SELECT 
        sp.IdSeccion,
        m.Nombre AS Seccion,
        sp.IdProfesor,
        CONCAT(p.Nombre, ' ', p.Apellido1, ' ', p.Apellido2) AS Profesor
    FROM SeccionProfesor sp
    JOIN Secciones m ON sp.IdSeccion = m.Nombre
    JOIN Profesores p ON sp.IdProfesor = p.IdProfesor;
END