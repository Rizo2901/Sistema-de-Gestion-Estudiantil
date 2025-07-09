
-- ELIMINAR
CREATE   PROCEDURE Enfermedad.SP_Eliminar
    @IdEnfermedad UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Enfermedades
    WHERE IdEnfermedad = @IdEnfermedad;
END;