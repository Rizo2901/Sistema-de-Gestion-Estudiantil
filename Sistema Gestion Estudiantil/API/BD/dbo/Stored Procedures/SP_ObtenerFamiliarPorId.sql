CREATE PROCEDURE SP_ObtenerFamiliarPorId
    @IdFamiliar UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        IdFamiliar,
        IdEstudiante,
        Nombre,
        Apellido1,
        Apellido2,
        Direccion,
        Parentesco,
        TelefonoCelular,
        CorreoElectronico,
        Ocupacion,
        EstadoCivil,
        CantidadHijos,
        IdUsuario,
        ViveConEstudiante,
        EsResponsableLegal
    FROM Familiares
    WHERE IdFamiliar = @IdFamiliar
END