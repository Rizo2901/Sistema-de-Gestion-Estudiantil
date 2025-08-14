

CREATE PROCEDURE Horario.Sp_ObtenerHorarioPorId
    @IdHorario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdHorario, IdSeccion, IdMateria, IdProfesor, Dia, HoraInicio, HoraFin, Aula
    FROM [dbo].[Horarios]
    WHERE IdHorario = @IdHorario;
END;