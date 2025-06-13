
-- Obtener un registro por su PK compuesta
CREATE PROCEDURE SP_ObtenerProfesoresMateriasSeccionesPorId
    @IdProfesor UNIQUEIDENTIFIER,
    @IdMateria UNIQUEIDENTIFIER,
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdProfesor, IdMateria, IdSeccion
    FROM ProfesoresMateriasSecciones
    WHERE IdProfesor = @IdProfesor AND IdMateria = @IdMateria AND IdSeccion = @IdSeccion;
END