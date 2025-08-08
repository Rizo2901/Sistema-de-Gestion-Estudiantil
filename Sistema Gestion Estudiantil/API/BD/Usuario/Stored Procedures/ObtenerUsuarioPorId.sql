
CREATE PROCEDURE [Usuario].[ObtenerUsuarioPorId]
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  
        U.IdUsuario,
        U.NombreUsuario,
        U.Contrasena,
        U.CorreoElectronico,
        U.IdRol,
        U.Estado,
        U.FechaActivacion,
        U.TipoEntidad,
        R.Nombre AS NombreRol,
        R.Descripcion AS DescripcionRol
    FROM [dbo].[Usuarios] U
    LEFT JOIN [dbo].[Roles] R ON U.IdRol = R.IdRol
    WHERE U.IdUsuario = @IdUsuario;
END