CREATE PROCEDURE SP_CrearFamiliar
    @IdEstudiante UNIQUEIDENTIFIER,
    @Nombre VARCHAR(255),
    @Apellido1 VARCHAR(255),
    @Apellido2 VARCHAR(255),
    @Direccion VARCHAR(MAX),
    @Parentesco VARCHAR(10),
    @TelefonoCelular VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(100) = NULL,
    @Ocupacion VARCHAR(100) = NULL,
    @EstadoCivil VARCHAR(10),
    @CantidadHijos INT = 0,
    @IdUsuario UNIQUEIDENTIFIER = NULL,
    @ViveConEstudiante BIT = 0,
    @EsResponsableLegal BIT = 0
AS
BEGIN
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();
    INSERT INTO Familiares (
        IdFamiliar, IdEstudiante, Nombre, Apellido1, Apellido2, Direccion, Parentesco,
        TelefonoCelular, CorreoElectronico, Ocupacion, EstadoCivil, CantidadHijos,
        IdUsuario, ViveConEstudiante, EsResponsableLegal
    )
    VALUES (
        @NewId, @IdEstudiante, @Nombre, @Apellido1, @Apellido2, @Direccion, @Parentesco,
        @TelefonoCelular, @CorreoElectronico, @Ocupacion, @EstadoCivil, @CantidadHijos,
        @IdUsuario, @ViveConEstudiante, @EsResponsableLegal
    );
    SELECT @NewId AS IdFamiliar;
END