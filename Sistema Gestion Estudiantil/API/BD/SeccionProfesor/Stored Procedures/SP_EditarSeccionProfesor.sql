CREATE PROCEDURE SeccionProfesor.SP_EditarSeccionProfesor
    @IdProfesorSeccion UNIQUEIDENTIFIER,
    @IdProfesor UNIQUEIDENTIFIER,
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.SeccionProfesor
    SET 
        IdProfesor = @IdProfesor,
        IdSeccion = @IdSeccion
    WHERE IdProfesorSeccion = @IdProfesorSeccion;

    SELECT @IdProfesorSeccion AS IdEditado;
END