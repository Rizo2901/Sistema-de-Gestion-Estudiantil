-- DELETE
CREATE PROCEDURE SP_EliminarAtencionMedica
    @IdAtencionMedica UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM AtencionesMedicas
    WHERE IdAtencionMedica = @IdAtencionMedica
END