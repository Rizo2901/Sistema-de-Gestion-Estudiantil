CREATE   PROCEDURE [EstudianteAlergia].SP_CrearAlergia
    @IdCategoria UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Alergias (IdAlergia, IdCategoria, Nombre, Descripcion)
    VALUES (@NuevoId, @IdCategoria, @Nombre, @Descripcion);

    SELECT @NuevoId AS IdAlergia;
END;