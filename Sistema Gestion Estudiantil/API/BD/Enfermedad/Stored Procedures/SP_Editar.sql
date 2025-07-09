
-- EDITAR
CREATE   PROCEDURE Enfermedad.SP_Editar
    @IdEnfermedad UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Enfermedades
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE IdEnfermedad = @IdEnfermedad;
END;