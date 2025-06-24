CREATE PROCEDURE SP_ObtenerHorarioPorId
    @IdHorario UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        IdHorario,
        Dia,
        HoraInicio,
        HoraFin,
        IdSeccion
    FROM Horarios
    WHERE IdHorario = @IdHorario
END
