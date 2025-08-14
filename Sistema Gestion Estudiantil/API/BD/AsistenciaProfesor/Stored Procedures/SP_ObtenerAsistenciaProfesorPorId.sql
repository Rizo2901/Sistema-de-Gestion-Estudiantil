
-- Obtener por ID
CREATE PROCEDURE AsistenciaProfesor.SP_ObtenerAsistenciaProfesorPorId
    @IdAsistenciaProfesor UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * 
    FROM AsistenciaProfesores
    WHERE IdAsistenciaProfesor = @IdAsistenciaProfesor;
END