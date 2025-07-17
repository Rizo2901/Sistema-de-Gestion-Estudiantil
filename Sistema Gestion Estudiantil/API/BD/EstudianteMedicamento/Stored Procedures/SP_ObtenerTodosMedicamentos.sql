
-- OBTENER TODOS
CREATE   PROCEDURE [EstudianteMedicamento].SP_ObtenerTodosMedicamentos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdMedicamento, Nombre, Descripcion
    FROM Medicamentos
    ORDER BY Nombre;
END;