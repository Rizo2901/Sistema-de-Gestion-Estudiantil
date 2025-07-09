
-- =============================
-- ELIMINAR
-- =============================
CREATE   PROCEDURE [Estudiante_Discapacidad].SP_EliminarEstudianteDiscapacidad
    @IdEstudianteDiscapacidad UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Estudiante_Discapacidad
    WHERE IdEstudianteDiscapacidad = @IdEstudianteDiscapacidad;
END;