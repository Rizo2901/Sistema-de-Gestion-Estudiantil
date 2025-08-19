CREATE PROCEDURE Seccion.sp_ObtenerEstudiantesPorSeccion
    @IdSeccion UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        IdEstudiante,
        Cedula,
        Nombre,
        Apellido1,
        Apellido2,
        FechaNacimiento,
        Genero,
        GradoActual,
        Repitiendo,
        HistorialAcademico,
        Fotografia,
        Direccion,
        TelefonoCelular,
        CorreoElectronico,
        Estado,
        IdSeccion,
        IdUsuario
    FROM Estudiantes
    WHERE IdSeccion = @IdSeccion
END