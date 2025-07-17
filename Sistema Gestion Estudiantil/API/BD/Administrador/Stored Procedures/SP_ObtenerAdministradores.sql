CREATE PROCEDURE [Administrador].[SP_ObtenerAdministradores]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        A.IdAdministrador,
        A.Nombre,
        A.Apellido1,
        A.Apellido2,
        A.Direccion,
        A.TelefonoCelular,
        A.CorreoElectronico,
        A.IdUsuario,
        U.NombreUsuario,
        U.CorreoElectronico AS UsuarioCorreo,
        U.TipoEntidad
    FROM [dbo].[Administradores] A
    LEFT JOIN [dbo].[Usuarios] U ON A.IdUsuario = U.IdUsuario;
END