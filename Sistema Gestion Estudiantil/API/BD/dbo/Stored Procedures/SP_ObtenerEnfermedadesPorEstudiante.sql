
-- OBTENER POR ESTUDIANTE
CREATE   PROCEDURE SP_ObtenerEnfermedadesPorEstudiante
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    SELECT EE.IdEnfermedad, E.Nombre, E.Descripcion,
           EE.FechaDiagnostico, EE.Gravedad, EE.Observaciones
    FROM Estudiante_Enfermedad EE
    JOIN Enfermedades E ON EE.IdEnfermedad = E.IdEnfermedad
    WHERE EE.IdEstudiante = @IdEstudiante;
END;