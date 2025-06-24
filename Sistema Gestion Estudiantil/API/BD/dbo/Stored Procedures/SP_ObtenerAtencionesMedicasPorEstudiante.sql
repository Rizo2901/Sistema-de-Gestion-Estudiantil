CREATE PROCEDURE SP_ObtenerAtencionesMedicasPorEstudiante
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM AtencionesMedicas
    WHERE IdEstudiante = @IdEstudiante
    ORDER BY Fecha DESC
END