namespace Abstracciones.Modelos
{
    // =============================
    // BASE: estructura común (campos del estudiante)
    // =============================
    public class ReporteEstudiantilBase
    {
        public Guid IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Seccion { get; set; }

        public string? Alergia { get; set; }
        public string? CategoriaAlergia { get; set; }

        public string? Discapacidad { get; set; }
        public string? CategoriaDiscapacidad { get; set; }

        public string? CondicionEspecial { get; set; }
        public string? CategoriaCondicionEspecial { get; set; }
        public DateTime? FechaDiagnosticoCondicion { get; set; }

        public string? Enfermedad { get; set; }
        public string? Medicamento { get; set; }

        public string? TipoAdaptacion { get; set; }
        public DateTime? FechaAdaptacion { get; set; }

        public string? TipoDocumento { get; set; }

        public string? MotivoReunion { get; set; }
        public DateTime? FechaReunion { get; set; }

        public string? SituacionFamiliar { get; set; }
        public string? CategoriaSituacionFamiliar { get; set; }
        public DateTime? FechaRegistroSituacionFamiliar { get; set; }

        public decimal? Calificacion { get; set; }
        public decimal? Promedio { get; set; }

        public DateTime? FechaAsistencia { get; set; }
        public string? EstadoAsistencia { get; set; }

        public string? ObservacionComportamiento { get; set; }
        public string? AccionTomadaComportamiento { get; set; }
        public DateTime? FechaAccionComportamiento { get; set; }

        public DateTime? FechaAtencionMedica { get; set; }
    }

    // =============================
    // RESPONSE: lo que devuelve la API
    // =============================
    namespace Abstracciones.Modelos
    {
        public class ReporteEstudiantilResponse
        {
            public Guid IdEstudiante { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Seccion { get; set; }

            // Alergias
            public string? Alergia { get; set; }
            public string? CategoriaAlergia { get; set; }
            public int CantidadAlergias { get; set; }

            // Discapacidad
            public string? Discapacidad { get; set; }
            public string? CategoriaDiscapacidad { get; set; }
            public int CantidadDiscapacidades { get; set; }

            // Enfermedades
            public string? Enfermedad { get; set; }
            public int CantidadEnfermedades { get; set; }

            // Medicamentos
            public string? Medicamento { get; set; }
            public int CantidadMedicamentos { get; set; }

            // Familiares
            public int CantidadFamiliares { get; set; }
            public int CantidadPadres { get; set; }
            public int CantidadMadres { get; set; }
            public int ViveConAmbosPadres { get; set; }
            public int ViveConUnPadre { get; set; }

            // Situación familiar
            public string? SituacionFamiliar { get; set; }
            public string? CategoriaSituacionFamiliar { get; set; }
            public DateTime? FechaRegistroSituacionFamiliar { get; set; }

            // Condición especial
            public string? CondicionEspecial { get; set; }
            public string? CategoriaCondicionEspecial { get; set; }
            public DateTime? FechaDiagnosticoCondicion { get; set; }

            // Calificación
            public decimal? Calificacion { get; set; }

            // Asistencia
            public int CantidadAusencias { get; set; }
            public int CantidadAsistencias { get; set; }
            public DateTime? FechaAsistencia { get; set; }
            public string? EstadoAsistencia { get; set; }

            // Comportamiento
            public string? ObservacionComportamiento { get; set; }
            public string? AccionTomadaComportamiento { get; set; }
            public DateTime? FechaAccionComportamiento { get; set; }

            // Atención médica
            public DateTime? FechaAtencionMedica { get; set; }

            // Documentos
            public string? TipoDocumento { get; set; }

            // Adaptaciones
            public string? TipoAdaptacion { get; set; }
            public DateTime? FechaAdaptacion { get; set; }

            // Reuniones
            public string? MotivoReunion { get; set; }
            public DateTime? FechaReunion { get; set; }

            // Promedio
            public decimal? Promedio { get; set; }

            // Campo adicional calculado (si lo usás)
            public string? EstadoGeneral { get; set; }
        }
    }

    // =============================
    // REQUEST: entrada dinámica para reportes
    // =============================
    public class ReporteEstudiantilRequest
    {
        public string? Alergia { get; set; }
        public string? CategoriaAlergia { get; set; }
        public string? Discapacidad { get; set; }
        public string? CategoriaDiscapacidad { get; set; }
        public string? CondicionEspecial { get; set; }
        public string? CategoriaCondicionEspecial { get; set; }
        public string? Enfermedad { get; set; }
        public string? Medicamento { get; set; }
        public string? Seccion { get; set; }                 // <-- aquí
        public string? CategoriaSituacionFamiliar { get; set; }
        public string? EstadoAsistencia { get; set; }
        public string? TipoDocumento { get; set; }
        public string? TipoAdaptacion { get; set; }
        public string? MotivoReunion { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public double? CalificacionLimite { get; set; }
        public string? ExportarComo { get; set; }           // PDF, Excel, CSV
        public List<string>? ColumnasSeleccionadas { get; set; }  // columnas que admin quiere ver
    }

}
