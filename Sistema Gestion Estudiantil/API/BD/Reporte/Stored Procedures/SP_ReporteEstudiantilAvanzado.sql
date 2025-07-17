CREATE PROCEDURE [Reporte].[SP_ReporteEstudiantilAvanzado]
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
    @FechaHasta DATE = NULL,
    @TipoReporte NVARCHAR(50) = 'detalle' -- 🧩 Nuevo parámetro para estilo de reporte
AS
BEGIN
    SET NOCOUNT ON;

    -- 📄 Modo detalle: listado plano por estudiante
    IF @TipoReporte = 'detalle'
    BEGIN
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
                @FechaDesde IS NULL OR (
                    FechaDiagnosticoCondicion >= @FechaDesde OR
                    FechaAccionComportamiento >= @FechaDesde OR
                    FechaAtencionMedica >= @FechaDesde
                )
            )
            AND (
                @FechaHasta IS NULL OR (
                    FechaDiagnosticoCondicion <= @FechaHasta OR
                    FechaAccionComportamiento <= @FechaHasta OR
                    FechaAtencionMedica <= @FechaHasta
                )
            )
        ORDER BY Seccion, Apellidos;
    END

    -- 📊 Modo resumen: total de estudiantes por categoría de alergia
    ELSE IF @TipoReporte = 'resumenAlergia'
    BEGIN
        SELECT 
            CategoriaAlergia,
            COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE Alergia IS NOT NULL AND CategoriaAlergia IS NOT NULL
        GROUP BY CategoriaAlergia
        ORDER BY TotalEstudiantes DESC;
    END

    -- ♿ Modo resumen: total por categoría de discapacidad
    ELSE IF @TipoReporte = 'resumenDiscapacidad'
    BEGIN
        SELECT 
            CategoriaDiscapacidad,
            COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE Discapacidad IS NOT NULL AND CategoriaDiscapacidad IS NOT NULL
        GROUP BY CategoriaDiscapacidad
        ORDER BY TotalEstudiantes DESC;
    END

    -- 🔍 Modo resumen: estudiantes por situación familiar
    ELSE IF @TipoReporte = 'resumenFamiliar'
    BEGIN
        SELECT 
            CategoriaSituacionFamiliar,
            COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE CategoriaSituacionFamiliar IS NOT NULL
        GROUP BY CategoriaSituacionFamiliar
        ORDER BY TotalEstudiantes DESC;
    END
END;