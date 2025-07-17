
-- 5. Crear procedimiento: EliminarAdministrador
CREATE PROCEDURE [Administrador].[SP_EliminarAdministrador]
    @IdAdministrador UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[Administradores]
    WHERE IdAdministrador = @IdAdministrador;
END