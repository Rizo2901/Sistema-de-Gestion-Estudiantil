
-- ELIMINAR
CREATE   PROCEDURE SP_EliminarMedicamento
    @IdMedicamento UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Medicamentos
    WHERE IdMedicamento = @IdMedicamento;
END;