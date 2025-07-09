CREATE   PROCEDURE [EstudianteAlergia].SP_EditarAlergia
    @IdAlergia UNIQUEIDENTIFIER,
    @IdCategoria UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    UPDATE Alergias
    SET IdCategoria = @IdCategoria,
        Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE IdAlergia = @IdAlergia;
END;