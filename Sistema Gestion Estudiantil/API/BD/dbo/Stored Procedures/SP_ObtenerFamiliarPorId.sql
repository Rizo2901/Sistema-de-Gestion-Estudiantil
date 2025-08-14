CREATE PROCEDURE [dbo].[SP_ObtenerFamiliarPorId]
    @IdFamiliar UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        IdFamiliar,
        IdEstudiante,
        Nombre,
        Apellido1,
        Apellido2,
		Cedula,
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