CREATE PROCEDURE SP_ObtenerMateriasPorProfesor
    @IdProfesor UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdProfesor, IdMateria, IdSeccion
    FROM ProfesoresMateriasSecciones
    WHERE IdProfesor = @IdProfesor;
END