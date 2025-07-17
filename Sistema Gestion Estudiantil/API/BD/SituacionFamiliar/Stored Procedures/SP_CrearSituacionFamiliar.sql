
-- CRUD Situaciones Familiares

CREATE PROCEDURE [SituacionFamiliar].[SP_CrearSituacionFamiliar]
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdCategoria UNIQUEIDENTIFIER,
	@FechaRegistro DATETIME,
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IdSituacion UNIQUEIDENTIFIER = NEWID();
    INSERT INTO dbo.SituacionFamiliar (IdSituacion, IdEstudiante, IdCategoria,FechaRegistro, Descripcion)
    VALUES (@IdSituacion, @IdEstudiante, @IdCategoria, @FechaRegistro, @Descripcion);
    SELECT @IdSituacion AS IdSituacionCreada;
END