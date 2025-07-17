-- SP_EliminarComportamiento
CREATE PROCEDURE SP_EliminarComportamiento @IdComportamiento UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Comportamiento WHERE IdComportamiento = @IdComportamiento;
    SELECT @IdComportamiento AS IdComportamiento;
END