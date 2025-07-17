CREATE   PROCEDURE Reporte.SP_ReporteEstudiantilAvanzado
    @Alergia NVARCHAR(100) = NULL,
    @Discapacidad NVARCHAR(100) = NULL,
    @CondicionEspecial NVARCHAR(100) = NULL,
    @Enfermedad NVARCHAR(100) = NULL,
    @Medicamento NVARCHAR(100) = NULL,
    @Seccion NVARCHAR(100) = NULL,
    @CategoriaAlergia NVARCHAR(100) = NULL,
    @CategoriaDiscapacidad NVARCHAR(100) = NULL,
    @CategoriaCondicionEspecial NVARCHAR(100) = NULL,
    @CategoriaSituacionFamiliar NVARCHAR(100) = NULL,
    @FechaDesde DATE = NULL,
    @FechaHasta DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM vw_PerfilEstudiantilCompleto
    WHERE
        (@Alergia IS NULL OR Alergia = @Alergia)
        AND (@CategoriaAlergia IS NULL OR CategoriaAlergia = @CategoriaAlergia)
        AND (@Discapacidad IS NULL OR Discapacidad = @Discapacidad)
        AND (@CategoriaDiscapacidad IS NULL OR CategoriaDiscapacidad = @CategoriaDiscapacidad)
        AND (@CondicionEspecial IS NULL OR CategoriaCondicionEspecial = @CondicionEspecial)
        AND (@CategoriaCondicionEspecial IS NULL OR CategoriaCondicionEspecial = @CategoriaCondicionEspecial)
        AND (@Enfermedad IS NULL OR Enfermedad = @Enfermedad)
        AND (@Medicamento IS NULL OR Medicamento = @Medicamento)
        AND (@Seccion IS NULL OR Seccion = @Seccion)
        AND (@CategoriaSituacionFamiliar IS NULL OR CategoriaSituacionFamiliar = @CategoriaSituacionFamiliar)
        AND (
            @FechaDesde IS NULL OR 
            (
                FechaDiagnosticoCondicion >= @FechaDesde OR
                FechaAccionComportamiento >= @FechaDesde OR
                FechaAtencionMedica >= @FechaDesde
            )
        )
        AND (
            @FechaHasta IS NULL OR 
            (
                FechaDiagnosticoCondicion <= @FechaHasta OR
                FechaAccionComportamiento <= @FechaHasta OR
                FechaAtencionMedica <= @FechaHasta
            )
        )
END;