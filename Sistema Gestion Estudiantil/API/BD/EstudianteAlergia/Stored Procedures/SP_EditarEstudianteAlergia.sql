
-- =============================
-- EDITAR
-- =============================
CREATE   PROCEDURE [EstudianteAlergia].SP_EditarEstudianteAlergia
    @IdEstudianteAlergia UNIQUEIDENTIFIER,
    @FechaDeteccion DATE,
    @Gravedad VARCHAR(50),
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Estudiante_Alergia
    SET FechaDeteccion = @FechaDeteccion,
        Gravedad = @Gravedad,
        Observaciones = @Observaciones
    WHERE IdEstudianteAlergia = @IdEstudianteAlergia;
END;