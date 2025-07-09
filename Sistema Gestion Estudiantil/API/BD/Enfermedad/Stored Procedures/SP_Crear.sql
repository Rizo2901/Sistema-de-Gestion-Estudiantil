-- CREAR
CREATE   PROCEDURE Enfermedad.SP_Crear
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Enfermedades (IdEnfermedad, Nombre, Descripcion)
    VALUES (@NuevoId, @Nombre, @Descripcion);

    SELECT @NuevoId AS IdEnfermedad;
END;