
-- ELIMINAR
CREATE   PROCEDURE SP_EliminarEstudianteEnfermedad
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdEnfermedad UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Estudiante_Enfermedad
    WHERE IdEstudiante = @IdEstudiante AND IdEnfermedad = @IdEnfermedad;
END;