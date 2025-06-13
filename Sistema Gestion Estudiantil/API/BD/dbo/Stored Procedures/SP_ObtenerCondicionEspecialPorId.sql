
CREATE PROCEDURE SP_ObtenerCondicionEspecialPorId
(
    @IdCondicion UNIQUEIDENTIFIER
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
    WHERE IdCondicion = @IdCondicion;
END;