CREATE PROCEDURE SP_ObtenerCondicionesEspecialesPorEstudiante
(
    @IdEstudiante UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdCondicion,
        IdEstudiante,
        TipoCondicion,
        NombreCondicion,
        FechaDiagnostico,
        Observaciones
    FROM CondicionesEspeciales
    WHERE IdEstudiante = @IdEstudiante;
END;