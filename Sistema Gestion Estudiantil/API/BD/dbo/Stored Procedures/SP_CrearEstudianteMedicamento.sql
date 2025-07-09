CREATE   PROCEDURE SP_CrearEstudianteMedicamento
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdMedicamento UNIQUEIDENTIFIER,
    @Dosis VARCHAR(50),
    @Frecuencia VARCHAR(50),
    @FechaInicio DATE,
    @FechaFin DATE,
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Estudiante_Medicamento (
        IdEstudiante, IdMedicamento,
        Dosis, Frecuencia, FechaInicio, FechaFin, Observaciones
    )
    VALUES (
        @IdEstudiante, @IdMedicamento,
        @Dosis, @Frecuencia, @FechaInicio, @FechaFin, @Observaciones
    );
END;