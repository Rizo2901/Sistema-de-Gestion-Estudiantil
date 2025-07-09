
-- EDITAR
CREATE   PROCEDURE [CondicionEspecial].SP_EditarCondicionEspecial
    @IdCondicion UNIQUEIDENTIFIER,
    @IdCategoria UNIQUEIDENTIFIER,
    @FechaDiagnostico DATE,
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CondicionesEspeciales
    SET IdCategoria = @IdCategoria,
        FechaDiagnostico = @FechaDiagnostico,
        Observaciones = @Observaciones
    WHERE IdCondicion = @IdCondicion;
END;