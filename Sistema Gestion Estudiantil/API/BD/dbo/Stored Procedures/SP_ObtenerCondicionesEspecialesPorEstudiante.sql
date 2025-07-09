
-- OBTENER POR ESTUDIANTE
CREATE   PROCEDURE SP_ObtenerCondicionesEspecialesPorEstudiante
    @IdEstudiante UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT CE.IdCondicion, CE.FechaDiagnostico, CE.Observaciones,
           C.Nombre AS Categoria, C.Descripcion AS CategoriaDescripcion
    FROM CondicionesEspeciales CE
    JOIN CategoriasCondicionEspecial C ON CE.IdCategoria = C.IdCategoria
    WHERE CE.IdEstudiante = @IdEstudiante;
END;