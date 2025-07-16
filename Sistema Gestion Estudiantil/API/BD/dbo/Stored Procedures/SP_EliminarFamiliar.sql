CREATE PROCEDURE SP_EliminarFamiliar
    @IdFamiliar UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Familiares WHERE IdFamiliar = @IdFamiliar;
    SELECT @IdFamiliar AS IdFamiliar;
END