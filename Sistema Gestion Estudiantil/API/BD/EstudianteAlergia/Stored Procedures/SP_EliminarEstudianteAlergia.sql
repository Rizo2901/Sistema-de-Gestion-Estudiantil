
-- =============================
-- ELIMINAR
-- =============================
CREATE   PROCEDURE [EstudianteAlergia].SP_EliminarEstudianteAlergia
    @IdEstudianteAlergia UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Estudiante_Alergia
    WHERE IdEstudianteAlergia = @IdEstudianteAlergia;
END;