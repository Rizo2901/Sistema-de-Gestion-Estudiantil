CREATE PROCEDURE [dbo].[SP_EditarFamiliar]
    @IdFamiliar UNIQUEIDENTIFIER,
    @IdEstudiante UNIQUEIDENTIFIER,
    @Nombre VARCHAR(255),
	@Cedula VARCHAR (40),
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
    UPDATE Familiares
    SET
        IdEstudiante = @IdEstudiante,
        Nombre = @Nombre,
		Cedula= @Cedula,
        Apellido1 = @Apellido1,
        Apellido2 = @Apellido2,
        Direccion = @Direccion,
        Parentesco = @Parentesco,
        TelefonoCelular = @TelefonoCelular,
        CorreoElectronico = @CorreoElectronico,
        Ocupacion = @Ocupacion,
        EstadoCivil = @EstadoCivil,
        CantidadHijos = @CantidadHijos,
        IdUsuario = @IdUsuario,
        ViveConEstudiante = @ViveConEstudiante,
        EsResponsableLegal = @EsResponsableLegal
    WHERE IdFamiliar = @IdFamiliar;
    SELECT @IdFamiliar AS IdFamiliar;
END