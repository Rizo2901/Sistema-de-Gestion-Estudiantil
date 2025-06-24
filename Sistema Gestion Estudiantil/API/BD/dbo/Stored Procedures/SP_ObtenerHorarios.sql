CREATE PROCEDURE SP_ObtenerHorarios
AS
BEGIN
    SELECT 
        IdHorario,
        Dia,
        HoraInicio,
        HoraFin,
        IdSeccion
    FROM Horarios
END
