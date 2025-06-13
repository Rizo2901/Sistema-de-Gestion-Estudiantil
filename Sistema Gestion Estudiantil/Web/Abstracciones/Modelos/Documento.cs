using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abstracciones.Modelos
{
    public class DocumentosBase
    {
        [Required]
        public Guid IdEstudiante { get; set; }

        [Required]
        [MaxLength(50)]
        public string TipoDocumento { get; set; }

        public string? Descripcion { get; set; }

        [Required]
        public string Archivo { get; set; }

        public string? TipoArchivo { get; set; }
    }

    public class DocumentosRequest : DocumentosBase
    {
        [Required]
        public Guid IdEstudiante { get; set; }

    }

    public class DocumentosResponse : DocumentosBase
    {
        public Guid IdDocumento { get; set; }
        public Guid IdEstudiante { get; set; }
        public new string? Archivo { get; set; }// <-- byte[] para lecturas
    }

    public class EstudianteConDocumentosResponse
    {
        public Guid IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        public List<DocumentosResponse> Documentos { get; set; } = new List<DocumentosResponse>();

    }

    public class EstudianteConDocumentosRaw
    {
        public Guid IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        [Column("Documentos")] // Dapper necesita este nombre exacto para mapear correctamente
        public string? Documentos { get; set; }
    }

}
