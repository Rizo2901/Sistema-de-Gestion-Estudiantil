
CREATE   PROCEDURE [EstudianteAlergia].SP_EliminarAlergia
    @IdAlergia UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Alergias
    WHERE IdAlergia = @IdAlergia;
END;