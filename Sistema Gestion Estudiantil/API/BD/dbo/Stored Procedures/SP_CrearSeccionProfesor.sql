
CREATE PROCEDURE SP_CrearSeccionProfesor
    @IdSeccion UNIQUEIDENTIFIER,
    @IdProfesor UNIQUEIDENTIFIER
AS
BEGIN
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO SeccionProfesor(IdProfesorSeccion, IdSeccion, IdProfesor)
    VALUES (@NuevoId,@IdSeccion, @IdProfesor);

	SELECT @NuevoId AS IdProfesorSeccion;
END