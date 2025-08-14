CREATE PROCEDURE AsistenciaProfesor.SP_ObtenerAsistenciasPorProfesor
    @IdProfesor UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM AsistenciaProfesores
    WHERE IdProfesor = @IdProfesor
    ORDER BY Fecha DESC;
END