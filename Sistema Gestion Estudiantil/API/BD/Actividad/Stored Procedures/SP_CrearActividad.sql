CREATE PROCEDURE [Actividad].[SP_CrearActividad]
    @Titulo VARCHAR(255),
    @Descripcion TEXT,
    @Fecha DATE,
    @FechaInicio DATETIME,
    @FechaFin DATETIME,
    @Tipo VARCHAR(15)
AS
BEGIN
    DECLARE @NuevoId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Actividades (
        IdActividad, Titulo, Descripcion, Fecha, FechaInicio, FechaFin, Tipo
    )
    VALUES (
        @NuevoId, @Titulo, @Descripcion, @Fecha, @FechaInicio, @FechaFin, @Tipo
    );

    SELECT @NuevoId AS IdActividad;
END;