CREATE PROCEDURE SP_EliminarEstudiante
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Estudiantes
    WHERE IdEstudiante = @IdEstudiante
END