Create PROCEDURE [Materia].[SP_ObtenerMateriaPorId]
    @IdMateria UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM Materias WHERE IdMateria = @IdMateria
END;