
-- ELIMINAR
CREATE   PROCEDURE [CondicionEspecial].SP_EliminarCondicionEspecial
    @IdCondicion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM CondicionesEspeciales
    WHERE IdCondicion = @IdCondicion;
END;