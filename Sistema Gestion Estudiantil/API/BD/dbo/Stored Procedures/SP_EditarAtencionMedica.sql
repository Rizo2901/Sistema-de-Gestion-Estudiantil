-- UPDATE
CREATE PROCEDURE SP_EditarAtencionMedica
    @IdAtencionMedica UNIQUEIDENTIFIER,
    @Fecha DATE,
    @Diagnostico TEXT,
    @Tratamiento TEXT = NULL
AS
BEGIN
    UPDATE AtencionesMedicas
    SET Fecha = @Fecha,
        Diagnostico = @Diagnostico,
        Tratamiento = @Tratamiento
    WHERE IdAtencionMedica = @IdAtencionMedica
END