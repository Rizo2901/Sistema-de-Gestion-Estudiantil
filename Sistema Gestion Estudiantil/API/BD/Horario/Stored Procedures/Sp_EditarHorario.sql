

CREATE PROCEDURE Horario.Sp_EditarHorario
    @IdHorario UNIQUEIDENTIFIER,
    @IdSeccion UNIQUEIDENTIFIER,
    @IdMateria UNIQUEIDENTIFIER,
    @IdProfesor UNIQUEIDENTIFIER,
    @Dia NVARCHAR(50),
    @HoraInicio TIME,
    @HoraFin TIME,
    @Aula NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Horarios]
    SET IdSeccion = @IdSeccion,
        IdMateria = @IdMateria,
        IdProfesor = @IdProfesor,
        Dia = @Dia,
        HoraInicio = @HoraInicio,
        HoraFin = @HoraFin,
        Aula = @Aula
    WHERE IdHorario = @IdHorario;
END;