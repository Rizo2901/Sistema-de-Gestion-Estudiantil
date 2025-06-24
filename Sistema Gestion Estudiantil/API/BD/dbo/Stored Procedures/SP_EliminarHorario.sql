CREATE PROCEDURE SP_EliminarHorario
    @IdHorario UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Horarios
    WHERE IdHorario = @IdHorario;

    SELECT @IdHorario AS IdHorario;
END
