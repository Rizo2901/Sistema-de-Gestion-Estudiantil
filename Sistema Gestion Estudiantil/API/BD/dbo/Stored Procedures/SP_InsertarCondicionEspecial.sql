CREATE PROCEDURE SP_InsertarCondicionEspecial
(
    @IdCondicion UNIQUEIDENTIFIER OUTPUT,
    @IdEstudiante UNIQUEIDENTIFIER,
    @TipoCondicion VARCHAR(50),
    @NombreCondicion VARCHAR(100),
    @FechaDiagnostico DATE = NULL,
    @Observaciones TEXT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @IdCondicion IS NULL
        SET @IdCondicion = NEWID();

    INSERT INTO CondicionesEspeciales
    (
        IdCondicion,
        IdEstudiante,
        TipoCondicion,
        NombreCondicion,
        FechaDiagnostico,
        Observaciones
    )
    VALUES
    (
        @IdCondicion,
        @IdEstudiante,
        @TipoCondicion,
        @NombreCondicion,
        @FechaDiagnostico,
        @Observaciones
    );

    SELECT @IdCondicion AS IdCondicion;
END;