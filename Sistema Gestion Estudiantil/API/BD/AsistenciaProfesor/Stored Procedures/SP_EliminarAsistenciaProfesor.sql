
-- Eliminar
CREATE PROCEDURE AsistenciaProfesor.SP_EliminarAsistenciaProfesor
    @IdAsistenciaProfesor UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM AsistenciaProfesores
    WHERE IdAsistenciaProfesor = @IdAsistenciaProfesor;
END