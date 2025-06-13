
-- Actualizar un registro (aunque no tiene sentido actualizar claves PK, te dejo ejemplo para si agregas columnas)
-- Por ahora solo ejemplo para cambiar Seccion para un profesor y materia
CREATE PROCEDURE SP_ActualizarProfesoresMateriasSecciones
    @IdProfesor UNIQUEIDENTIFIER,
    @IdMateria UNIQUEIDENTIFIER,
    @IdSeccionAntigua UNIQUEIDENTIFIER,
    @IdSeccionNueva UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE ProfesoresMateriasSecciones
    SET IdSeccion = @IdSeccionNueva
    WHERE IdProfesor = @IdProfesor AND IdMateria = @IdMateria AND IdSeccion = @IdSeccionAntigua;
    
    SELECT @IdProfesor AS IdProfesor, @IdMateria AS IdMateria, @IdSeccionNueva AS IdSeccion;
END