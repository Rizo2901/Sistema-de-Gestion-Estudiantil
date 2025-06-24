CREATE PROCEDURE SP_EliminarProfesor
    @IdProfesor UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Profesores
    WHERE IdProfesor = @IdProfesor;
END;