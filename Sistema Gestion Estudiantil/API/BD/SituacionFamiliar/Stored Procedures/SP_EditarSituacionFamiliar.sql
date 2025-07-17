
CREATE PROCEDURE [SituacionFamiliar].[SP_EditarSituacionFamiliar]
    @IdSituacion UNIQUEIDENTIFIER,
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdCategoria UNIQUEIDENTIFIER,
	@FechaRegistro DATETIME,
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.SituacionFamiliar
    SET IdEstudiante = @IdEstudiante,
        IdCategoria = @IdCategoria,
		FechaRegistro = @FechaRegistro,
        Descripcion = @Descripcion
    WHERE IdSituacion = @IdSituacion;
    SELECT @IdSituacion AS IdSituacionEditada;
END