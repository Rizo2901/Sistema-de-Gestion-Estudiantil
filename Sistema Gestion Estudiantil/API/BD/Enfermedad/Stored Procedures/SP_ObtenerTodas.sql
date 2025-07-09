
-- OBTENER TODAS
CREATE   PROCEDURE Enfermedad.SP_ObtenerTodas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdEnfermedad, Nombre, Descripcion
    FROM Enfermedades;
END;