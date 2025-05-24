CREATE PROCEDURE SP_ObtenerEstudiantePorId
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM Estudiantes
    WHERE IdEstudiante = @IdEstudiante
END