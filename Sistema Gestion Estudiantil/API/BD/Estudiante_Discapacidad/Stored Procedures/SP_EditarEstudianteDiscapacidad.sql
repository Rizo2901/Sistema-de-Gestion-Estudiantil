CREATE   PROCEDURE [Estudiante_Discapacidad].[SP_EditarEstudianteDiscapacidad]
    @IdEstudianteDiscapacidad UNIQUEIDENTIFIER,
    @FechaDiagnostico DATE,
    @GradoAfectacion VARCHAR(50),
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Estudiante_Discapacidad
    SET FechaDiagnostico = @FechaDiagnostico,
        GradoAfectacion = @GradoAfectacion,
        Observaciones = @Observaciones
    WHERE IdEstudianteDiscapacidad = @IdEstudianteDiscapacidad;
END;