
-- OBTENER POR ID
CREATE   PROCEDURE SP_ObtenerMedicamentoPorId
    @IdMedicamento UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdMedicamento, Nombre, Descripcion
    FROM Medicamentos
    WHERE IdMedicamento = @IdMedicamento;
END;