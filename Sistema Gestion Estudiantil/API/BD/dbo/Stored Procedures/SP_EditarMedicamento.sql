
-- EDITAR
CREATE   PROCEDURE SP_EditarMedicamento
    @IdMedicamento UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Descripcion TEXT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Medicamentos
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE IdMedicamento = @IdMedicamento;
END;