-- Crear Discapacidad
CREATE   PROCEDURE [Discapacidad].SP_CrearDiscapacidad
    @IdCategoria UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Discapacidades (IdDiscapacidad, IdCategoria, Nombre, Descripcion)
    VALUES (@NuevoId, @IdCategoria, @Nombre, @Descripcion);

    SELECT @NuevoId AS IdDiscapacidad;
END;