-- SP_EditarComportamiento
CREATE PROCEDURE SP_EditarComportamiento
    @IdComportamiento UNIQUEIDENTIFIER,
    @IdEstudiante UNIQUEIDENTIFIER,
    @Fecha DATE,
    @Observacion TEXT = NULL,
    @AccionTomada TEXT = NULL
AS
BEGIN
    UPDATE Comportamiento
    SET IdEstudiante = @IdEstudiante,
        Fecha = @Fecha,
        Observacion = @Observacion,
        AccionTomada = @AccionTomada
    WHERE IdComportamiento = @IdComportamiento;
    SELECT @IdComportamiento AS IdComportamiento;
END