
-- SP: Actualizar Materia
CREATE PROCEDURE SP_ActualizarMateria
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