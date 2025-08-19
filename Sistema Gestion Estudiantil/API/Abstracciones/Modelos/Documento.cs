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

        public string Archivo { get; set; }

        public string? TipoArchivo { get; set; }
    }

    public class DocumentosRequest : DocumentosBase
    {
        [Required]
        public Guid IdEstudiante { get; set; }

    }

    public class DocumentosResponse
    {
        public Guid IdEstudiante { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        // Y los campos del documento:
        public Guid IdDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string Descripcion { get; set; }
        public string Archivo { get; set; }
        public string TipoArchivo { get; set; }
    }
    public class EstudianteConDocumentos
{
    public Guid IdEstudiante { get; set; }
    public string Cedula { get; set; }
    public string Nombre { get; set; }
    public string Apellido1 { get; set; }
    public string Apellido2 { get; set; }

    public List<DocumentoDetalle> Documentos { get; set; }
}

    public class DocumentoDetalle
    {
        public Guid IdDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string Descripcion { get; set; }
        public string Archivo { get; set; }
        public string TipoArchivo { get; set; }
    }

}
