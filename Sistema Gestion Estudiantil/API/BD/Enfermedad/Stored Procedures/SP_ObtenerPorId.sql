
-- OBTENER POR ID
CREATE   PROCEDURE Enfermedad.SP_ObtenerPorId
    @IdEnfermedad UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdEnfermedad, Nombre, Descripcion
    FROM Enfermedades
    WHERE IdEnfermedad = @IdEnfermedad;
END;