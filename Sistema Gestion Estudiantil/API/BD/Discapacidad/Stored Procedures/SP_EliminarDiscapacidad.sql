
CREATE   PROCEDURE [Discapacidad].SP_EliminarDiscapacidad
    @IdDiscapacidad UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Discapacidades
    WHERE IdDiscapacidad = @IdDiscapacidad;
END;