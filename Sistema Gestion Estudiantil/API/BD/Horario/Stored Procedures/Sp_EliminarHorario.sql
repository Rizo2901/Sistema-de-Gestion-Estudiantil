

CREATE PROCEDURE Horario.Sp_EliminarHorario
    @IdHorario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[Horarios]
    WHERE IdHorario = @IdHorario;
END;