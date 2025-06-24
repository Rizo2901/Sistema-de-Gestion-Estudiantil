CREATE PROCEDURE SP_ObtenerProfesoresPorSeccion
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        p.IdProfesor,
        CONCAT(p.Nombre, ' ', p.Apellido1, ' ', p.Apellido2) AS NombreProfesor
    FROM SeccionProfesor sp
    JOIN Profesores p ON sp.IdProfesor = p.IdProfesor
    WHERE sp.IdSeccion = @IdSeccion;
END