CREATE PROCEDURE sp_ProfesoresPorMateria
    @IdMateria UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        p.IdProfesor,
        CONCAT(p.Nombre, ' ', p.Apellido1, ' ', p.Apellido2) AS NombreProfesor
    FROM MateriaProfesor mp
    JOIN Profesores p ON mp.IdProfesor = p.IdProfesor
    WHERE mp.IdMateria = @IdMateria;
END