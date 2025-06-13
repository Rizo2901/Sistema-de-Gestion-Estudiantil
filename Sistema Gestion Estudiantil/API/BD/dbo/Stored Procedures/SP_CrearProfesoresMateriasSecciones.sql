-- Crear un registro
CREATE PROCEDURE SP_CrearProfesoresMateriasSecciones
    @IdProfesor UNIQUEIDENTIFIER,
    @IdMateria UNIQUEIDENTIFIER,
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO ProfesoresMateriasSecciones (IdProfesor, IdMateria, IdSeccion)
    VALUES (@IdProfesor, @IdMateria, @IdSeccion);
    
    -- Retornar la PK compuesta como un único GUID concatenado o NULL (a definir) - 
    -- aquí por simplicidad retorno NULL (no tiene sentido un GUID único compuesto)
    SELECT @IdProfesor AS IdProfesor, @IdMateria AS IdMateria, @IdSeccion AS IdSeccion;
END