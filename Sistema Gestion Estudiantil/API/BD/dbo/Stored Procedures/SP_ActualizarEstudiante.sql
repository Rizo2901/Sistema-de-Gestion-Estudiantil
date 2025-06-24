CREATE PROCEDURE [dbo].[SP_ActualizarEstudiante] (
    @IdEstudiante UNIQUEIDENTIFIER,
    @Cedula VARCHAR(20),
    @Nombre VARCHAR(255),
    @Apellido1 VARCHAR(255),
    @Apellido2 VARCHAR(255),
    @FechaNacimiento DATE,
    @Genero VARCHAR(10),
    @GradoActual VARCHAR(20),
    @Repitiendo BIT = 0,
    @HistorialAcademico TEXT = NULL,
    @Fotografia VARCHAR(MAX) = NULL,
    @Direccion TEXT = NULL,
    @TelefonoCelular VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(100) = NULL,
    @Estado BIT = 1,
    @IdSeccion UNIQUEIDENTIFIER = NULL,
    @IdUsuario UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Estudiantes
    SET
        Cedula = @Cedula,
        Nombre = @Nombre,
        Apellido1 = @Apellido1,
        Apellido2 = @Apellido2,
        FechaNacimiento = @FechaNacimiento,
        Genero = @Genero,
        GradoActual = @GradoActual,
        Repitiendo = @Repitiendo,
        HistorialAcademico = @HistorialAcademico,
        Fotografia = @Fotografia,
        Direccion = @Direccion,
        TelefonoCelular = @TelefonoCelular,
        CorreoElectronico = @CorreoElectronico,
        Estado = @Estado,
        IdSeccion = @IdSeccion,
        IdUsuario = @IdUsuario
    WHERE IdEstudiante = @IdEstudiante;
END;