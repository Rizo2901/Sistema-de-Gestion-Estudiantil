
CREATE PROCEDURE SituacionFamiliar.SP_EditarSituacionFamiliar
    @IdSituacion UNIQUEIDENTIFIER,
    @IdEstudiante UNIQUEIDENTIFIER,
    @IdCategoria UNIQUEIDENTIFIER,
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.SituacionFamiliar
    SET IdEstudiante = @IdEstudiante,
        IdCategoria = @IdCategoria,
        Descripcion = @Descripcion
    WHERE IdSituacion = @IdSituacion;
    SELECT @IdSituacion AS IdSituacionEditada;
END