
-- SP SIMPLIFICADO: filtra solo por una columna de fecha relevante
CREATE PROCEDURE [Reporte].[SP_ReporteEstudiantilAvanzado]
    @Alergia NVARCHAR(100) = NULL,
    @CategoriaAlergia NVARCHAR(100) = NULL,
    @Discapacidad NVARCHAR(100) = NULL,
    @CategoriaDiscapacidad NVARCHAR(100) = NULL,
    @CondicionEspecial NVARCHAR(100) = NULL,
    @CategoriaCondicionEspecial NVARCHAR(100) = NULL,
    @Enfermedad NVARCHAR(100) = NULL,
    @Medicamento NVARCHAR(100) = NULL,
    @Seccion NVARCHAR(100) = NULL,
    @CategoriaSituacionFamiliar NVARCHAR(100) = NULL,
    @EstadoAsistencia NVARCHAR(20) = NULL,
    @TipoDocumento NVARCHAR(50) = NULL,
    @TipoAdaptacion NVARCHAR(50) = NULL,
    @MotivoReunion NVARCHAR(100) = NULL,
    @PromedioMinimo DECIMAL(5,2) = NULL,
    @FechaDesde DATE = NULL,
    @FechaHasta DATE = NULL,
    @TipoReporte NVARCHAR(50) = 'detalle'
AS
BEGIN
    SET NOCOUNT ON;

    IF @TipoReporte = 'detalle'
    BEGIN
        SELECT *
        FROM vw_PerfilEstudiantilCompleto
        WHERE
            (@Alergia IS NULL OR Alergia = @Alergia)
            AND (@CategoriaAlergia IS NULL OR CategoriaAlergia = @CategoriaAlergia)
            AND (@Discapacidad IS NULL OR Discapacidad = @Discapacidad)
            AND (@CategoriaDiscapacidad IS NULL OR CategoriaDiscapacidad = @CategoriaDiscapacidad)
            AND (@CondicionEspecial IS NULL OR CAST(CondicionEspecial AS NVARCHAR(MAX)) = @CondicionEspecial)
            AND (@CategoriaCondicionEspecial IS NULL OR CategoriaCondicionEspecial = @CategoriaCondicionEspecial)
            AND (@Enfermedad IS NULL OR Enfermedad = @Enfermedad)
            AND (@Medicamento IS NULL OR Medicamento = @Medicamento)
            AND (@Seccion IS NULL OR Seccion = @Seccion)
            AND (@CategoriaSituacionFamiliar IS NULL OR CategoriaSituacionFamiliar = @CategoriaSituacionFamiliar)
            AND (@EstadoAsistencia IS NULL OR EstadoAsistencia = @EstadoAsistencia)
            AND (@TipoDocumento IS NULL OR TipoDocumento = @TipoDocumento)
            AND (@TipoAdaptacion IS NULL OR TipoAdaptacion = @TipoAdaptacion)
            AND (@MotivoReunion IS NULL OR CAST(MotivoReunion AS NVARCHAR(MAX)) = @MotivoReunion)
            AND (@PromedioMinimo IS NULL OR Promedio >= @PromedioMinimo)
            AND (@FechaDesde IS NULL OR FechaRegistroSituacionFamiliar >= @FechaDesde)
            AND (@FechaHasta IS NULL OR FechaRegistroSituacionFamiliar <= @FechaHasta)
        ORDER BY Seccion, Apellidos;
    END

    -- Los demás casos de resumen se mantienen igual
    ELSE IF @TipoReporte = 'resumenAlergia'
    BEGIN
        SELECT CategoriaAlergia, COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE CategoriaAlergia IS NOT NULL
        GROUP BY CategoriaAlergia
        ORDER BY TotalEstudiantes DESC;
    END

    ELSE IF @TipoReporte = 'resumenDiscapacidad'
    BEGIN
        SELECT CategoriaDiscapacidad, COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE CategoriaDiscapacidad IS NOT NULL
        GROUP BY CategoriaDiscapacidad
        ORDER BY TotalEstudiantes DESC;
    END

    ELSE IF @TipoReporte = 'resumenCondicionEspecial'
    BEGIN
        SELECT CategoriaCondicionEspecial, COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE CategoriaCondicionEspecial IS NOT NULL
        GROUP BY CategoriaCondicionEspecial
        ORDER BY TotalEstudiantes DESC;
    END

    ELSE IF @TipoReporte = 'resumenFamiliar'
    BEGIN
        SELECT CategoriaSituacionFamiliar, COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE CategoriaSituacionFamiliar IS NOT NULL
        GROUP BY CategoriaSituacionFamiliar
        ORDER BY TotalEstudiantes DESC;
    END

    ELSE IF @TipoReporte = 'resumenAsistencia'
    BEGIN
        SELECT EstadoAsistencia, COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE EstadoAsistencia IS NOT NULL
        GROUP BY EstadoAsistencia
        ORDER BY TotalEstudiantes DESC;
    END

    ELSE IF @TipoReporte = 'resumenDocumentos'
    BEGIN
        SELECT TipoDocumento, COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE TipoDocumento IS NOT NULL
        GROUP BY TipoDocumento
        ORDER BY TotalEstudiantes DESC;
    END

    ELSE IF @TipoReporte = 'resumenAdaptaciones'
    BEGIN
        SELECT TipoAdaptacion, COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE TipoAdaptacion IS NOT NULL
        GROUP BY TipoAdaptacion
        ORDER BY TotalEstudiantes DESC;
    END

    ELSE IF @TipoReporte = 'resumenReuniones'
    BEGIN
        SELECT CAST(MotivoReunion AS NVARCHAR(MAX)) AS MotivoReunion, COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE MotivoReunion IS NOT NULL
        GROUP BY CAST(MotivoReunion AS NVARCHAR(MAX))
        ORDER BY TotalEstudiantes DESC;
    END

    ELSE IF @TipoReporte = 'resumenPromedio'
    BEGIN
        SELECT 
            CASE 
                WHEN Promedio >= 90 THEN 'Excelente (90-100)'
                WHEN Promedio >= 70 THEN 'Bueno (70-89)'
                WHEN Promedio >= 50 THEN 'Regular (50-69)'
                ELSE 'Bajo (<50)'
            END AS RangoPromedio,
            COUNT(DISTINCT IdEstudiante) AS TotalEstudiantes
        FROM vw_PerfilEstudiantilCompleto
        WHERE Promedio IS NOT NULL
        GROUP BY 
            CASE 
                WHEN Promedio >= 90 THEN 'Excelente (90-100)'
                WHEN Promedio >= 70 THEN 'Bueno (70-89)'
                WHEN Promedio >= 50 THEN 'Regular (50-69)'
                ELSE 'Bajo (<50)'
            END
        ORDER BY TotalEstudiantes DESC;
    END
END;