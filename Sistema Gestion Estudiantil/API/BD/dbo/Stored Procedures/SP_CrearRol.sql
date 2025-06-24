CREATE PROCEDURE [dbo].[SP_CrearRol]
    @Nombre VARCHAR(50),
    @Descripcion VARCHAR(255)
AS
BEGIN
     DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();


    INSERT INTO Roles (IdRol, Nombre, Descripcion)
    VALUES (@NuevoId, @Nombre, @Descripcion);

	SELECT @NuevoId AS IdRol;
END;