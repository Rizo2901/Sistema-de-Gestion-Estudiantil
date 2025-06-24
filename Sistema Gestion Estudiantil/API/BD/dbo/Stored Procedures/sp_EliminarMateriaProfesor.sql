CREATE PROCEDURE sp_EliminarMateriaProfesor
    @IdMateria UNIQUEIDENTIFIER,
    @IdProfesor UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM MateriaProfesor
    WHERE IdMateria = @IdMateria AND IdProfesor = @IdProfesor;
END