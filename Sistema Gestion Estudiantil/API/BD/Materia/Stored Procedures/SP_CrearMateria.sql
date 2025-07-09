Create PROCEDURE [Materia].[SP_CrearMateria]
      @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Materias (IdMateria, Nombre, Descripcion)
    VALUES (@NuevoId, @Nombre, @Descripcion);

    SELECT @NuevoId AS IdMateria;
END;