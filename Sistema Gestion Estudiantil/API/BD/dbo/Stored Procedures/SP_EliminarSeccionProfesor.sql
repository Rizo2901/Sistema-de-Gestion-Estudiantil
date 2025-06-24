
CREATE PROCEDURE SP_EliminarSeccionProfesor
    @IdSeccion UNIQUEIDENTIFIER,
    @IdProfesor UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM SeccionProfesor
    WHERE IdSeccion = @IdSeccion AND IdProfesor = @IdProfesor;
END