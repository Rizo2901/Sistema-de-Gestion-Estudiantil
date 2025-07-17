namespace Abstracciones.Modelos
{
    // =============================
    // BASE: estructura común
    // =============================
    namespace Abstracciones.Modelos
    {
        public class ReporteEstudiantilBase
        {
            public Guid IdEstudiante { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Seccion { get; set; }

            public string? Alergia { get; set; }
            public string? DescripcionAlergia { get; set; }
            public string? CategoriaAlergia { get; set; }
            public string? DescripcionCategoriaAlergia { get; set; }

            public string? Discapacidad { get; set; }
            public string? DescripcionDiscapacidad { get; set; }
            public string? CategoriaDiscapacidad { get; set; }
            public string? DescripcionCategoriaDiscapacidad { get; set; }

            public string? CondicionEspecial { get; set; }
            public string? ObservacionCondicion { get; set; }
            public DateTime? FechaDiagnosticoCondicion { get; set; }
            public string? CategoriaCondicionEspecial { get; set; }
            public string? DescripcionCategoriaCondicion { get; set; }

            public string? Enfermedad { get; set; }
            public string? DescripcionEnfermedad { get; set; }

            public string? Medicamento { get; set; }
            public string? DescripcionMedicamento { get; set; }

            public string? TipoAdaptacion { get; set; }

            public string? SituacionFamiliar { get; set; }
            public string? DescripcionSituacionFamiliar { get; set; }
            public string? CategoriaSituacionFamiliar { get; set; }
            public string? DescripcionCategoriaSituacionFamiliar { get; set; }
            public DateTime? FechaRegistroSituacionFamiliar { get; set; }

            public decimal? Calificacion { get; set; }

            public DateTime? FechaAsistencia { get; set; }
            public string? EstadoAsistencia { get; set; }

            public string? ObservacionComportamiento { get; set; }
            public string? AccionTomadaComportamiento { get; set; }
            public DateTime? FechaAccionComportamiento { get; set; }

            public DateTime? FechaAtencionMedica { get; set; }
        }

        // =============================
        // REQUEST: filtros para el reporte
        // =============================
        public class ReporteEstudiantilRequest
        {
            public string? Alergia { get; set; }
            public string? Discapacidad { get; set; }
            public string? Enfermedad { get; set; }
            public string? Medicamento { get; set; }
            public string? Seccion { get; set; }
            public string? CondicionEspecial { get; set; }
            public string? SituacionFamiliar { get; set; }

            public bool SoloConAlergia { get; set; } = false;
            public bool SoloConDiscapacidad { get; set; } = false;
            public bool SoloConEnfermedad { get; set; } = false;
            public bool SoloConMedicamento { get; set; } = false;

            public DateTime? FechaDesde { get; set; }
            public DateTime? FechaHasta { get; set; }
            public string? CategoriaAlergia { get; set; }
            public string? CategoriaDiscapacidad { get; set; }
            public string? CategoriaCondicionEspecial { get; set; }
            public string? CategoriaSituacionFamiliar { get; set; }
            public string? ExportarComo { get; set; } // "pdf", "excel", "csv"

            public string? Operacion { get; set; } // Ej: "conteo", "porcentaje", "promedio", "agrupado"
            public string? CampoAgrupacion { get; set; } // Ej: "Seccion", "CategoriaAlergia", etc.
            public double? CalificacionLimite { get; set; } // Para porcentajes
        }

        // =============================
        // RESPONSE: datos del reporte
        // =============================
        public class ReporteEstudiantilResponse : ReporteEstudiantilBase
        {

            public string? EstadoGeneral { get; set; } // ej: "Estable", "Crítico"
        }
    }
}
