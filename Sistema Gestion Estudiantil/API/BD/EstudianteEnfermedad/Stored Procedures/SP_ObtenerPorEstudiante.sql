
-- =============================
-- OBTENER POR ESTUDIANTE
-- =============================
CREATE   PROCEDURE EstudianteEnfermedad.SP_ObtenerPorEstudiante
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT EE.IdEstudianteEnfermedad,
           EE.IdEnfermedad,
           E.Nombre,
           E.Descripcion,
           EE.FechaDiagnostico,
           EE.Gravedad,
           EE.Observaciones
    FROM Estudiante_Enfermedad EE
    JOIN Enfermedades E ON EE.IdEnfermedad = E.IdEnfermedad
    WHERE EE.IdEstudiante = @IdEstudiante;
END;