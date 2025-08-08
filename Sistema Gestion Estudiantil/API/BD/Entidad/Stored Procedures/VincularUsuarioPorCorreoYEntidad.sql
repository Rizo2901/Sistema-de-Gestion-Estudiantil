  CREATE PROCEDURE [Entidad].[VincularUsuarioPorCorreoYEntidad]
    @CorreoElectronico NVARCHAR(100),
    @IdEntidad UNIQUEIDENTIFIER,
    @TipoEntidad NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdUsuario UNIQUEIDENTIFIER;

    SELECT @IdUsuario = IdUsuario
    FROM Usuarios
    WHERE CorreoElectronico = @CorreoElectronico;

    IF @IdUsuario IS NULL
    BEGIN
        RAISERROR('No existe un usuario con ese correo.', 16, 1);
        RETURN;
    END

    IF @TipoEntidad = 'Profesor'
        UPDATE Profesores SET IdUsuario = @IdUsuario WHERE IdProfesor = @IdEntidad;
    ELSE IF @TipoEntidad = 'Estudiante'
        UPDATE Estudiantes SET IdUsuario = @IdUsuario WHERE IdEstudiante = @IdEntidad;
    ELSE IF @TipoEntidad = 'Familiar'
        UPDATE Familiares SET IdUsuario = @IdUsuario WHERE IdFamiliar = @IdEntidad;
    ELSE IF @TipoEntidad = 'Administrador'
        UPDATE Administradores SET IdUsuario = @IdUsuario WHERE IdAdministrador = @IdEntidad;
    ELSE
        RAISERROR('Tipo de entidad no reconocido.', 16, 1);
END