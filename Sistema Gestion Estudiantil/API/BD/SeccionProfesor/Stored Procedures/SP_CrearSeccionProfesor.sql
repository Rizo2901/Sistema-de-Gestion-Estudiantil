CREATE PROCEDURE SeccionProfesor.SP_CrearSeccionProfesor
    @IdProfesor UNIQUEIDENTIFIER,
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdProfesorSeccion UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.SeccionProfesor (IdProfesorSeccion, IdProfesor, IdSeccion)
    VALUES (@IdProfesorSeccion, @IdProfesor, @IdSeccion);

    SELECT @IdProfesorSeccion AS IdCreado;
END