
CREATE PROCEDURE [dbo].[SP_InsertarEstudiante] (
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
    @IdUsuario UNIQUEIDENTIFIER = NULL,
    @IdEstudiante UNIQUEIDENTIFIER OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @IdEstudiante IS NULL
        SET @IdEstudiante = NEWID();

    INSERT INTO Estudiantes (
        IdEstudiante,
        Cedula, Nombre, Apellido1, Apellido2, FechaNacimiento,
        Genero, GradoActual, Repitiendo, HistorialAcademico,
        Fotografia, Direccion, TelefonoCelular, CorreoElectronico, Estado,
        IdSeccion, IdUsuario
    )
    VALUES (
        @IdEstudiante,
        @Cedula, @Nombre, @Apellido1, @Apellido2, @FechaNacimiento,
        @Genero, @GradoActual, @Repitiendo, @HistorialAcademico,
        @Fotografia, @Direccion, @TelefonoCelular, @CorreoElectronico, @Estado,
        @IdSeccion, @IdUsuario
    );

    SELECT @IdEstudiante AS IdEstudiante;
END;