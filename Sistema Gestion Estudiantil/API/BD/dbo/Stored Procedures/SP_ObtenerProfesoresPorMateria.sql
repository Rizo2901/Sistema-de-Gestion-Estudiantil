CREATE PROCEDURE SP_ObtenerProfesoresPorMateria
    @IdMateria UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdProfesor, IdMateria, IdSeccion
    FROM ProfesoresMateriasSecciones
    WHERE IdMateria = @IdMateria;
END