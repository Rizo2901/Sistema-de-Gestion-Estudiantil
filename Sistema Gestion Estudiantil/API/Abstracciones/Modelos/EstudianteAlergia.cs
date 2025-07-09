using System;
using System.Collections.Generic;

namespace Abstracciones.Modelos
{
    // Modelo base para CRUD
    public class EstudianteAlergiaBase
    {
        public Guid IdEstudianteAlergia { get; set; }
        public Guid IdEstudiante { get; set; }
        public Guid IdAlergia { get; set; }
        public DateTime FechaDeteccion { get; set; }
        public string Gravedad { get; set; }
        public string? Observaciones { get; set; }
    }

    // Modelo para crear/editar
    public class EstudianteAlergiaRequest : EstudianteAlergiaBase
    {
        public Guid IdEstudianteAlergia { get; set; }
    }

    // Modelo plano para vista completa
    public class EstudianteAlergiaResponse : EstudianteAlergiaBase
    {
        public Guid IdEstudianteAlergia { get; set; }
        public string NombreAlergia { get; set; }
        public string DescripcionAlergia { get; set; }

        public string NombreEstudiante { get; set; }
        public string Apellido1Estudiante { get; set; }
        public string Apellido2Estudiante { get; set; }

        public string Cedula { get; set; }
        public string GradoActual { get; set; }
        public string Genero { get; set; }

        public string Categoria { get; set; }
        public string CategoriaDescripcion { get; set; }
    }

    // Modelo crudo para datos con JSON anidado
    public class EstudianteConAlergiasRaw
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string GradoActual { get; set; }
        public string Genero { get; set; }
        public string Alergias { get; set; } // JSON string
    }

    // Modelo procesado para mostrar alergias anidadas
    public class EstudianteConAlergiasResponse
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string GradoActual { get; set; }
        public string Genero { get; set; }
        public List<AlergiaItem> Alergias { get; set; }
    }

    // Elemento dentro de la lista de alergias
    public class AlergiaItem
    {
        public Guid IdAlergia { get; set; }
        public string NombreAlergia { get; set; }
        public string DescripcionAlergia { get; set; }

        public string Categoria { get; set; }
        public string CategoriaDescripcion { get; set; }

        public DateTime FechaDeteccion { get; set; }
        public string Gravedad { get; set; }
        public string? Observaciones { get; set; }
    }
}