CREATE PROCEDURE sp_ActualizarMateriaProfesor
    @IdMateriaOld UNIQUEIDENTIFIER,
    @IdProfesorOld UNIQUEIDENTIFIER,
    @IdMateriaNew UNIQUEIDENTIFIER,
    @IdProfesorNew UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM MateriaProfesor
    WHERE IdMateria = @IdMateriaOld AND IdProfesor = @IdProfesorOld;

    INSERT INTO MateriaProfesor (IdMateria, IdProfesor)
    VALUES (@IdMateriaNew, @IdProfesorNew);
END