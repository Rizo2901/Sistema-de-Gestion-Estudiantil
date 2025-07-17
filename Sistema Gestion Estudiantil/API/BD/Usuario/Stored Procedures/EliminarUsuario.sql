
-- 5. Eliminar Usuario
CREATE PROCEDURE [Usuario].[EliminarUsuario]
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[Usuarios]
    WHERE IdUsuario = @IdUsuario;
END