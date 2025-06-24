-- READ (Por Id)
CREATE PROCEDURE SP_ObtenerAtencionMedica
    @IdAtencionMedica UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM AtencionesMedicas
    WHERE IdAtencionMedica = @IdAtencionMedica
END