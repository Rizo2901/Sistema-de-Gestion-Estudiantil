using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class EstudianteDiscapacidadBase
    {
        public Guid IdEstudianteDiscapacidad { get; set; }
        public Guid IdEstudiante { get; set; }
        public Guid IdDiscapacidad { get; set; }
        public string GradoAfectacion { get; set; }
        public DateTime? FechaDiagnostico { get; set; }
        public string Observaciones { get; set; }
    }
    public class EstudianteDiscapacidadRequest : EstudianteDiscapacidadBase
    {
        public Guid IdEstudianteDiscapacidad { get; set; }
    }
    public class EstudianteDiscapacidadResponse : EstudianteDiscapacidadBase
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string CategoriaDescripcion { get; set; }
        public string NombreDiscapacidad { get; set; }
        public string DescripcionDiscapacidad { get; set; }
        public string NombreCompleto { get; set; }

        public string GradoActual { get; set; }

        public string Discapacidades { get; set; }

        public string Cedula { get; set; }
    }

    public class EstudianteConDiscapacidadesResponse
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string GradoActual { get; set; }
        public string Genero { get; set; }

        public List<DiscapacidadItem> Discapacidades { get; set; }
    }

    public class EstudianteConDiscapacidadesRaw
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string GradoActual { get; set; }
        public string Genero { get; set; }

        public string Discapacidades { get; set; } // String JSON desde el SP
    }

    public class DiscapacidadItem
    {
        public Guid IdDiscapacidad { get; set; }
        public string DiscapacidadNombre { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaDescripcion { get; set; }
    }
}
