Create PROCEDURE [Materia].[SP_EditarMateria]
    @IdMateria UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    UPDATE Materias
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE IdMateria = @IdMateria
END;