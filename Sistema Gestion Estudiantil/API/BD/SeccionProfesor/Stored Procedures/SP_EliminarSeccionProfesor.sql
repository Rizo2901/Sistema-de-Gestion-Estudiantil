CREATE PROCEDURE SeccionProfesor.SP_EliminarSeccionProfesor
    @IdProfesorSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.SeccionProfesor
    WHERE IdProfesorSeccion = @IdProfesorSeccion;

    SELECT @IdProfesorSeccion AS IdEliminado;
END