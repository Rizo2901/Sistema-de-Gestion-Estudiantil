CREATE PROCEDURE SP_CrearComportamiento
    @IdEstudiante UNIQUEIDENTIFIER,
    @Fecha DATE,
    @Observacion TEXT = NULL,
    @AccionTomada TEXT = NULL
AS
BEGIN
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();
    INSERT INTO Comportamiento (IdComportamiento, IdEstudiante, Fecha, Observacion, AccionTomada)
    VALUES (@NewId, @IdEstudiante, @Fecha, @Observacion, @AccionTomada);
    SELECT @NewId AS IdComportamiento;
END