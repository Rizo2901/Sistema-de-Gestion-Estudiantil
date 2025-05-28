CREATE PROCEDURE SP_Crear_Seccion
    @Nombre VARCHAR(50)
AS
BEGIN
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Secciones (IdSeccion, Nombre)
    VALUES (@NuevoId, @Nombre);

    SELECT @NuevoId AS IdSeccion;
END;