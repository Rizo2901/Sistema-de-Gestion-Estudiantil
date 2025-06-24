CREATE PROCEDURE SP_ObtenerProfesorPorId
  @IdProfesor UNIQUEIDENTIFIER
  AS
  BEGIN
        SELECT * FROM Profesores WHERE IdProfesor = @IdProfesor
END;