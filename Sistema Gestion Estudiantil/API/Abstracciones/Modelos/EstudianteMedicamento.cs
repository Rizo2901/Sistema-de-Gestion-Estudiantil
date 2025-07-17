using System;
using System.Collections.Generic;

namespace Abstracciones.Modelos
{
    public class EstudianteMedicamentoBase
    {
        public Guid IdEstudianteMedicamento { get; set; }
        public Guid IdEstudiante { get; set; }
        public Guid IdMedicamento { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Observaciones { get; set; }
    }

    public class EstudianteMedicamentoRequest
    {
        public Guid IdEstudiante { get; set; }
        public Guid IdMedicamento { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Observaciones { get; set; }
    }
    public class EstudianteMedicamentoResponse : EstudianteMedicamentoBase
    {
        public string NombreMedicamento { get; set; }
        public string DescripcionMedicamento { get; set; }

        public string NombreEstudiante { get; set; }
        public string Apellido1Estudiante { get; set; }
        public string Apellido2Estudiante { get; set; }
    }

    public class EstudianteConMedicamentosRaw
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string GradoActual { get; set; }
        public string Genero { get; set; }

        public string Medicamentos { get; set; } // JSON serializado
    }

    public class EstudianteConMedicamentosResponse
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string GradoActual { get; set; }
        public string Genero { get; set; }

        public List<MedicamentoItem> Medicamentos { get; set; }
    }

   
    public class MedicamentoItem
    {
        public Guid IdEstudianteMedicamento { get; set; }
        public Guid IdMedicamento { get; set; }
        public string NombreMedicamento { get; set; }
        public string DescripcionMedicamento { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Observaciones { get; set; }
    }
}