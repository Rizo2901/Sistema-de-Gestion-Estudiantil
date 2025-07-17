
-- 3. Consultar todos los Usuarios
CREATE PROCEDURE [Usuario].[ConsultarUsuarios]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        U.IdUsuario,
        U.NombreUsuario,
        U.CorreoElectronico,
        U.IdRol,
        U.Estado,
        U.FechaActivacion,
        U.TipoEntidad,
        R.Nombre AS NombreRol,
        R.Descripcion AS DescripcionRol
    FROM [dbo].[Usuarios] U
    LEFT JOIN [dbo].[Roles] R ON U.IdRol = R.IdRol;
END