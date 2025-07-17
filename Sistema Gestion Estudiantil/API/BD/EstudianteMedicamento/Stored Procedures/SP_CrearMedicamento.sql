-- CREAR
CREATE   PROCEDURE [EstudianteMedicamento].SP_CrearMedicamento
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Medicamentos (IdMedicamento, Nombre, Descripcion)
    VALUES (@NuevoId, @Nombre, @Descripcion);

    SELECT @NuevoId AS IdMedicamento;
END;