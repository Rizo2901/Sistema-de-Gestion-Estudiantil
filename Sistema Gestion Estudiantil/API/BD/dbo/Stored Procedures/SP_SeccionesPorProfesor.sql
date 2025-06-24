
CREATE PROCEDURE SP_SeccionesPorProfesor
    @IdProfesor UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        s.IdSeccion,
        s.Nombre AS NombreSeccion
    FROM SeccionProfesor sp
    JOIN Secciones s ON sp.IdSeccion = S.IdSeccion
    WHERE sp.IdProfesor = @IdProfesor;
END