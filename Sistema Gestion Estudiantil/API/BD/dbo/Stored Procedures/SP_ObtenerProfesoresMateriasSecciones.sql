
-- Obtener todos los registros
CREATE PROCEDURE SP_ObtenerProfesoresMateriasSecciones
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdProfesor, IdMateria, IdSeccion
    FROM ProfesoresMateriasSecciones;
END