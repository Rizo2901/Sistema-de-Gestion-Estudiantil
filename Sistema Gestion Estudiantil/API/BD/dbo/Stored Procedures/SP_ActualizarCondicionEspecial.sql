CREATE PROCEDURE [dbo].[SP_ActualizarCondicionEspecial]
(
    @IdCondicion UNIQUEIDENTIFIER,
    @IdEstudiante UNIQUEIDENTIFIER,
    @TipoCondicion VARCHAR(50),
    @NombreCondicion VARCHAR(100),
    @FechaDiagnostico DATE = NULL,
    @Observaciones TEXT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CondicionesEspeciales
    SET
        IdEstudiante = @IdEstudiante,
        TipoCondicion = @TipoCondicion,
        NombreCondicion = @NombreCondicion,
        FechaDiagnostico = @FechaDiagnostico,
        Observaciones = @Observaciones
    WHERE IdCondicion = @IdCondicion;

    SELECT @IdCondicion AS Id; -- ✅ Esto lo devuelve al C#
END;