CREATE PROCEDURE SP_EditarSeccionProfesor
    @IdSeccionOld UNIQUEIDENTIFIER,
    @IdProfesorOld UNIQUEIDENTIFIER,
    @IdSeccionNew UNIQUEIDENTIFIER,
    @IdProfesorNew UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM SeccionProfesor
    WHERE IdSeccion = @IdSeccionOld AND IdProfesor = @IdProfesorOld;

    INSERT INTO SeccionProfesor(IdSeccion, IdProfesor)
    VALUES (@IdSeccionNew, @IdProfesorNew);
END