CREATE PROCEDURE sp_MateriasPorProfesor
    @IdProfesor UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        m.IdMateria,
        m.Nombre AS NombreMateria
    FROM MateriaProfesor mp
    JOIN Materias m ON mp.IdMateria = m.IdMateria
    WHERE mp.IdProfesor = @IdProfesor;
END