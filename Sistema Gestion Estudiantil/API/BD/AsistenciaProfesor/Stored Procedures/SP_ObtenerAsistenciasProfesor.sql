
-- Obtener todos
CREATE PROCEDURE AsistenciaProfesor.SP_ObtenerAsistenciasProfesor
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM AsistenciaProfesores;
END