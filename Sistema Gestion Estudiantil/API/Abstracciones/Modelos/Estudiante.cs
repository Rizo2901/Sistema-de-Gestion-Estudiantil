using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Abstracciones.Modelos
{

    public class EstudianteBase
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string GradoActual { get; set; }
        public bool Repitiendo { get; set; }
        public string HistorialAcademico { get; set; }
        public string Discapacidades { get; set; }
        public bool UsoLentes { get; set; }
        public string Enfermedades { get; set; }
        public string Alergias { get; set; }
        public byte[] Fotografia { get; set; }
        public string Direccion { get; set; }
        public string Contacto { get; set; }
        public bool Estado { get; set; }
        public int IdSeccion { get; set; }
        public int? IdUsuario { get; set; }

        
    }



}


