
-- Editar Discapacidad
CREATE   PROCEDURE [Discapacidad].SP_EditarDiscapacidad
    @IdDiscapacidad UNIQUEIDENTIFIER,
    @IdCategoria UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Discapacidades
    SET IdCategoria = @IdCategoria,
        Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE IdDiscapacidad = @IdDiscapacidad;
END;