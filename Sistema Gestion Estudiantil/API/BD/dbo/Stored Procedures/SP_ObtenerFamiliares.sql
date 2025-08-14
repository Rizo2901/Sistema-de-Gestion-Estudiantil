CREATE PROCEDURE [dbo].[SP_ObtenerFamiliares]
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
END