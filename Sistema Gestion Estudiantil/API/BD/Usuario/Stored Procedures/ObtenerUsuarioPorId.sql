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
        R.Descripcion AS DescripcionRol,
        -- Campos adicionales sugeridos
        E.NombreEntidad,
        U.FotoPerfil -- si existe ese campo en Usuarios
    FROM [dbo].[Usuarios] U
    LEFT JOIN [dbo].[Roles] R ON U.IdRol = R.IdRol
    LEFT JOIN [dbo].[Entidades] E ON U.TipoEntidad = E.TipoEntidad -- si tenés tabla de entidades
    WHERE U.IdUsuario = @IdUsuario;
END