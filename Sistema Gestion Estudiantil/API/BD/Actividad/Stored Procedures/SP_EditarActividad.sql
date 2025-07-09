CREATE PROCEDURE [Actividad].[SP_EditarActividad]
    @IdActividad UNIQUEIDENTIFIER,
    @Titulo VARCHAR(255),
    @Descripcion TEXT,
    @Fecha DATE,
    @Tipo VARCHAR(15)
AS
BEGIN
    UPDATE Actividades
    SET Titulo = @Titulo,
        Descripcion = @Descripcion,
        Fecha = @Fecha,
        Tipo = @Tipo
    WHERE IdActividad = @IdActividad;
END;