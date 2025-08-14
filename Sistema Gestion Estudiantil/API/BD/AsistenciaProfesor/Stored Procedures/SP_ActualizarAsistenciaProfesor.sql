
-- Actualizar
CREATE PROCEDURE AsistenciaProfesor.SP_ActualizarAsistenciaProfesor
    @IdAsistenciaProfesor UNIQUEIDENTIFIER,
    @IdProfesor UNIQUEIDENTIFIER,
    @Fecha DATE,
    @HoraEntrada TIME,
    @HoraSalida TIME
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE AsistenciaProfesores
    SET IdProfesor = @IdProfesor,
        Fecha = @Fecha,
        HoraEntrada = @HoraEntrada,
        HoraSalida = @HoraSalida
    WHERE IdAsistenciaProfesor = @IdAsistenciaProfesor;
END