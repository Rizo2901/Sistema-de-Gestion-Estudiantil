
-- SP: Eliminar Materia
CREATE PROCEDURE SP_EliminarMateria
    @IdMateria UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Materias WHERE IdMateria = @IdMateria
END;