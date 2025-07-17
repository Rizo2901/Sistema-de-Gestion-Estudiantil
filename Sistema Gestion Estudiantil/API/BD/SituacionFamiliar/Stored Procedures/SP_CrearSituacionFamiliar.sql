
-- CRUD Situaciones Familiares

CREATE PROCEDURE SituacionFamiliar.SP_CrearSituacionFamiliar
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdCategoria UNIQUEIDENTIFIER,
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IdSituacion UNIQUEIDENTIFIER = NEWID();
    INSERT INTO dbo.SituacionFamiliar (IdSituacion, IdEstudiante, IdCategoria, Descripcion)
    VALUES (@IdSituacion, @IdEstudiante, @IdCategoria, @Descripcion);
    SELECT @IdSituacion AS IdSituacionCreada;
END