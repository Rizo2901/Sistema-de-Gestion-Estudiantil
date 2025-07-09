
-- =============================
-- ELIMINAR
-- =============================
CREATE   PROCEDURE EstudianteEnfermedad.SP_Eliminar
    @IdEstudianteEnfermedad UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Estudiante_Enfermedad
    WHERE IdEstudianteEnfermedad = @IdEstudianteEnfermedad;
END;