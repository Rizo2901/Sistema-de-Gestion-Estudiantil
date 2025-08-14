CREATE PROCEDURE [Horario].[Sp_CrearHorario]
    @IdSeccion UNIQUEIDENTIFIER,
    @IdMateria UNIQUEIDENTIFIER,
    @IdProfesor UNIQUEIDENTIFIER,
    @Dia NVARCHAR(50),
    @HoraInicio TIME,
    @HoraFin TIME,
    @Aula NVARCHAR(50)
	AS
BEGIN
  SET NOCOUNT ON;

    DECLARE @NuevoIdHorario UNIQUEIDENTIFIER = NEWID();


    INSERT INTO [dbo].[Horarios] (
        IdHorario, IdSeccion, IdMateria, IdProfesor, Dia, HoraInicio, HoraFin, Aula
    )
    VALUES (
        @NuevoIdHorario, @IdSeccion, @IdMateria, @IdProfesor, @Dia, @HoraInicio, @HoraFin, @Aula
    );

	  SELECT @NuevoIdHorario;
END;