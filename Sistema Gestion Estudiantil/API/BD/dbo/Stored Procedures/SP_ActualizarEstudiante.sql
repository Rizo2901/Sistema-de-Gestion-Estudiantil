CREATE PROCEDURE SP_ActualizarEstudiante (
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
    @Discapacidades TEXT = NULL,
    @UsoLentes BIT = 0,
    @Enfermedades TEXT = NULL,
    @Alergias TEXT = NULL,
    @Fotografia VARBINARY(MAX) = NULL,
    @Direccion TEXT = NULL,
    @TelefonoCelular VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(100) = NULL,
    @Estado BIT = 1,
    @IdSeccion UNIQUEIDENTIFIER = NULL,
    @IdUsuario UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
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
        Discapacidades = @Discapacidades,
        UsoLentes = @UsoLentes,
        Enfermedades = @Enfermedades,
        Alergias = @Alergias,
        Fotografia = @Fotografia,
        Direccion = @Direccion,
        TelefonoCelular = @TelefonoCelular,
        CorreoElectronico = @CorreoElectronico,
        Estado = @Estado
    WHERE IdEstudiante = @IdEstudiante;

    IF @IdSeccion IS NOT NULL
    BEGIN
        UPDATE Estudiantes
        SET IdSeccion = @IdSeccion
        WHERE IdEstudiante = @IdEstudiante;
    END

    IF @IdUsuario IS NOT NULL
    BEGIN
        UPDATE Estudiantes
        SET IdUsuario = @IdUsuario
        WHERE IdEstudiante = @IdEstudiante;
    END
END;