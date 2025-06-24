CREATE PROCEDURE sp_ObtenerMateriasProfesores
AS
BEGIN
    SELECT 
        mp.IdMateria,
        m.Nombre AS Materia,
        mp.IdProfesor,
        CONCAT(p.Nombre, ' ', p.Apellido1, ' ', p.Apellido2) AS Profesor
    FROM MateriaProfesor mp
    JOIN Materias m ON mp.IdMateria = m.IdMateria
    JOIN Profesores p ON mp.IdProfesor = p.IdProfesor;
END