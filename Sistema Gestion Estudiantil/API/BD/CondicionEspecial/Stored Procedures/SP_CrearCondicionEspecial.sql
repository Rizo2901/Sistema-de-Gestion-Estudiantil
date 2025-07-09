-- CREAR
CREATE   PROCEDURE [CondicionEspecial].SP_CrearCondicionEspecial
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdCategoria UNIQUEIDENTIFIER,
    @FechaDiagnostico DATE,
    @Observaciones TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO CondicionesEspeciales (IdCondicion, IdEstudiante, IdCategoria, FechaDiagnostico, Observaciones)
    VALUES (@NuevoId, @IdEstudiante, @IdCategoria, @FechaDiagnostico, @Observaciones);

    SELECT @NuevoId AS IdCondicion;
END;