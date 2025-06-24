CREATE PROCEDURE SP_CrearHorario
    @Dia NVARCHAR(20),
    @HoraInicio TIME,
    @HoraFin TIME,
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Horarios (IdHorario, Dia, HoraInicio, HoraFin, IdSeccion)
    VALUES (@NewId, @Dia, @HoraInicio, @HoraFin, @IdSeccion);

    SELECT @NewId AS IdHorario;
END
