
CREATE PROCEDURE SP_ObtenerCondicionesEspeciales
(
    @IdEstudiante UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @IdEstudiante IS NULL
    BEGIN
        SELECT
            IdCondicion,
            IdEstudiante,
            TipoCondicion,
            NombreCondicion,
            FechaDiagnostico,
            Observaciones
        FROM CondicionesEspeciales;
    END
    ELSE
    BEGIN
        SELECT
            IdCondicion,
            IdEstudiante,
            TipoCondicion,
            NombreCondicion,
            FechaDiagnostico,
            Observaciones
        FROM CondicionesEspeciales
        WHERE IdEstudiante = @IdEstudiante;
    END
END;