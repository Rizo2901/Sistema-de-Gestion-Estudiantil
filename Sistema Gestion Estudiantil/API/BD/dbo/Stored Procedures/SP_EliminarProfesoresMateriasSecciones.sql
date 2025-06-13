
-- Eliminar un registro
CREATE PROCEDURE SP_EliminarProfesoresMateriasSecciones
    @IdProfesor UNIQUEIDENTIFIER,
    @IdMateria UNIQUEIDENTIFIER,
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM ProfesoresMateriasSecciones
    WHERE IdProfesor = @IdProfesor AND IdMateria = @IdMateria AND IdSeccion = @IdSeccion;

    SELECT @IdProfesor AS IdProfesor, @IdMateria AS IdMateria, @IdSeccion AS IdSeccion;
END