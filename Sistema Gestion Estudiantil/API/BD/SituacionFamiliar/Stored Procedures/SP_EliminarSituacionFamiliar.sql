
CREATE PROCEDURE SituacionFamiliar.SP_EliminarSituacionFamiliar
    @IdSituacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.SituacionFamiliar
    WHERE IdSituacion = @IdSituacion;
    SELECT @IdSituacion AS IdSituacionEliminada;
END