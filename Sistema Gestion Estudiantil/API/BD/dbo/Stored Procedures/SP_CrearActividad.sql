CREATE PROCEDURE SP_CrearActividad
    @Titulo VARCHAR(255),
    @Descripcion TEXT,
    @Fecha DATE,
    @Tipo VARCHAR(15)
AS
BEGIN
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Actividades (IdActividad, Titulo, Descripcion, Fecha, Tipo)
    VALUES (@NuevoId, @Titulo, @Descripcion, @Fecha, @Tipo);

    SELECT @NuevoId AS IdActividad;
END;