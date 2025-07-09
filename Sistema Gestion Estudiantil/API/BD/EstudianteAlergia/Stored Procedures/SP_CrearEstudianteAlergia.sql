
-- =============================
-- CREAR
-- =============================
CREATE   PROCEDURE [EstudianteAlergia].SP_CrearEstudianteAlergia
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdAlergia UNIQUEIDENTIFIER,
    @FechaDeteccion DATE,
    @Gravedad VARCHAR(50),
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Estudiante_Alergia (
        IdEstudianteAlergia, IdEstudiante, IdAlergia,
        FechaDeteccion, Gravedad, Observaciones
    )
    VALUES (
        @NuevoId, @IdEstudiante, @IdAlergia,
        @FechaDeteccion, @Gravedad, @Observaciones
    );

    SELECT @NuevoId AS IdEstudianteAlergia;
END;