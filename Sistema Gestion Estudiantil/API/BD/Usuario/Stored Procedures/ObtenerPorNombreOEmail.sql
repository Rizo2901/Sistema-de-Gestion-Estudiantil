CREATE PROCEDURE Usuario.ObtenerPorNombreOEmail
    @Identificador NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    SET @Identificador = LTRIM(RTRIM(@Identificador));

    SELECT
        u.IdUsuario,
        u.NombreUsuario,
        u.Contrasena,
        u.CorreoElectronico,
        u.IdRol,
        r.Nombre AS NombreRol,
        u.TipoEntidad
    FROM Usuarios u
    INNER JOIN Roles r ON u.IdRol = r.IdRol
    WHERE (u.NombreUsuario = @Identificador OR u.CorreoElectronico = @Identificador)
      AND u.Estado = 1;
END