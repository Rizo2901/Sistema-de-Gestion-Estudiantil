
-- Crear
CREATE PROCEDURE AsistenciaProfesor.SP_CrearAsistenciaProfesor
    @IdProfesor UNIQUEIDENTIFIER,
    @Fecha DATE,
    @HoraEntrada TIME,
    @HoraSalida TIME
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO AsistenciaProfesores (
        IdAsistenciaProfesor, IdProfesor, Fecha, HoraEntrada, HoraSalida
    )
    VALUES (
        @NewId, @IdProfesor, @Fecha, @HoraEntrada, @HoraSalida
    );

    SELECT @NewId AS IdAsistenciaProfesor;
END