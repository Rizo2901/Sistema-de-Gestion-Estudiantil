CREATE PROCEDURE SP_EditarHorario
    @IdHorario UNIQUEIDENTIFIER,
    @Dia NVARCHAR(20),
    @HoraInicio TIME,
    @HoraFin TIME,
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE Horarios
    SET
        Dia = @Dia,
        HoraInicio = @HoraInicio,
        HoraFin = @HoraFin,
        IdSeccion = @IdSeccion
    WHERE IdHorario = @IdHorario;

    SELECT @IdHorario AS IdHorario;
END
