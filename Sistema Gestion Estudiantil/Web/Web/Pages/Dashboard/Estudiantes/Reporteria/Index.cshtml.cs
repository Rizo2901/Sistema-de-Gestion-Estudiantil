using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.Estudiantes.Reporteria
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;

        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }

        [BindProperty] public ReporteEstudiantilRequest Filtros { get; set; } = new();
        public List<ReporteEstudiantilResponse> Resultado { get; set; } = new();
        public SelectList ColumnasDisponibles { get; set; }
        public string? PdfBase64 { get; set; }

        public List<SelectListItem> AlergiasDisponibles { get; set; }
        public List<SelectListItem> CategoriasAlergiaDisponibles { get; set; }
        public List<SelectListItem> DiscapacidadesDisponibles { get; set; }
        public List<SelectListItem> CategoriasDiscapacidadDisponibles { get; set; }
        public List<SelectListItem> EnfermedadesDisponibles { get; set; }
        public List<SelectListItem> MedicamentosDisponibles { get; set; }
        public List<SelectListItem> AdaptacionesDisponibles { get; set; }
        public List<SelectListItem> DocumentosDisponibles { get; set; }
        public List<SelectListItem> CategoriasCondicionDisponibles { get; set; }
        public List<SelectListItem> CategoriasSituacionDisponibles { get; set; }

        public async Task OnGetAsync()
        {
            var columnas = typeof(ReporteEstudiantilResponse).GetProperties()
                .Select(p => p.Name).ToList();

            ColumnasDisponibles = new SelectList(columnas);
            await CargarCatalogosAsync();
        }

        public async Task<IActionResult> OnPostGenerarAsync()
        {
            try
            {
                // 🔍 DEBUG: Mostrar endpoint seleccionado
                Console.WriteLine($"[DEBUG] Endpoint seleccionado: {(string.IsNullOrEmpty(Filtros.ExportarComo) ? "Vista" : "Universal")}");

                string endPoint = string.IsNullOrEmpty(Filtros.ExportarComo)
                    ? _confg.ObtenerMetodo("ApiEndPoints", "ReporteVista")
                    : _confg.ObtenerMetodo("ApiEndPoints", "ReporteUniversal");

                var jsonRequest = JsonSerializer.Serialize(Filtros, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

                // 🔍 DEBUG: Mostrar JSON enviado
                Console.WriteLine($"[DEBUG] JSON enviado:\n{jsonRequest}");

                using var cliente = new HttpClient();
                var contenido = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PostAsync(endPoint, contenido);

                // 🔍 DEBUG: Estado de respuesta
                Console.WriteLine($"[DEBUG] Código de respuesta: {respuesta.StatusCode}");

                if (!respuesta.IsSuccessStatusCode)
                {
                    var error = await respuesta.Content.ReadAsStringAsync();
                    Console.WriteLine($"[DEBUG] Error recibido: {error}");
                    ModelState.AddModelError(string.Empty, $"Error al generar el reporte: {respuesta.StatusCode} - {error}");
                    await CargarCatalogosAsync();
                    return Page();
                }

                if (string.IsNullOrEmpty(Filtros.ExportarComo))
                {
                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    Console.WriteLine($"[DEBUG] JSON recibido:\n{resultado}");

                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    Resultado = JsonSerializer.Deserialize<List<ReporteEstudiantilResponse>>(resultado, opciones);

                    var columnas = typeof(ReporteEstudiantilResponse).GetProperties()
                        .Select(p => p.Name).ToList();
                    ColumnasDisponibles = new SelectList(columnas);

                    await CargarCatalogosAsync();
                    return Page();
                }
                else
                {
                    var archivo = await respuesta.Content.ReadAsByteArrayAsync();
                    Console.WriteLine($"[DEBUG] Tamaño del archivo recibido: {archivo?.Length ?? 0} bytes");

                    if (archivo == null || archivo.Length == 0)
                    {
                        Console.WriteLine("[DEBUG] El archivo recibido está vacío.");
                        ModelState.AddModelError(string.Empty, "El archivo generado está vacío.");
                        await CargarCatalogosAsync();
                        return Page();
                    }

                    var tipo = Filtros.ExportarComo switch
                    {
                        "pdf" => "application/pdf",
                        "excel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "csv" => "text/csv",
                        _ => "application/octet-stream"
                    };
                    var nombre = $"Reporte.{Filtros.ExportarComo}";

                    Console.WriteLine($"[DEBUG] Tipo MIME: {tipo}, Nombre: {nombre}");
                    return File(archivo, tipo, nombre);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Excepción atrapada: {ex.Message}");
                ModelState.AddModelError(string.Empty, $"Error inesperado: {ex.Message}");
                await CargarCatalogosAsync();
                return Page();
            }
        }

        public async Task CargarCatalogosAsync()
        {
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            using var client = new HttpClient();

            async Task<List<SelectListItem>> Obtener<T>(string claveConfig, Func<T, string> texto, Func<T, string> valor)
            {
                string endpoint = _confg.ObtenerMetodo("ApiEndPoints", claveConfig);
                var respuesta = await client.GetAsync(endpoint);
                respuesta.EnsureSuccessStatusCode();
                var json = await respuesta.Content.ReadAsStringAsync();
                var items = JsonSerializer.Deserialize<List<T>>(json, opciones);
                return items.Select(s => new SelectListItem
                {
                    Value = valor(s),
                    Text = texto(s)
                }).ToList();
            }

            AlergiasDisponibles = await Obtener<AlergiaResponse>("ObtenerAlergias", s => s.Nombre, s => s.Nombre);
            CategoriasAlergiaDisponibles = await Obtener<CategoriaAlergiaResponse>("ObtenerCategoriasAlergias", s => s.Nombre, s => s.Nombre);
            DiscapacidadesDisponibles = await Obtener<DiscapacidadResponse>("ObtenerDiscapacidades", s => s.Nombre, s => s.Nombre);
            CategoriasDiscapacidadDisponibles = await Obtener<CategoriasDiscapacidadResponse>("ObtenerCategoriasDiscapacidades", s => s.Nombre, s => s.Nombre);
            EnfermedadesDisponibles = await Obtener<EnfermedadResponse>("ObtenerEnfermedades", s => s.Nombre, s => s.Nombre);
            MedicamentosDisponibles = await Obtener<MedicamentoResponse>("ObtenerMedicamentos", s => s.Nombre, s => s.Nombre);
            AdaptacionesDisponibles = await Obtener<AdaptacionCurricularResponse>("ObtenerAdaptacionesCurriculares", s => s.Tipo, s => s.Tipo);
            DocumentosDisponibles = await Obtener<DocumentosResponse>("ObtenerDocumentos", s => s.TipoDocumento, s => s.TipoDocumento);
            CategoriasCondicionDisponibles = await Obtener<CategoriasCondicionEspecialResponse>("ObtenerCategoriasCondiciones", s => $"{s.Nombre} {s.Descripcion}", s => s.Nombre);
            CategoriasSituacionDisponibles = await Obtener<CategoriasSituacionFamiliarResponse>("ObtenerCategoriasSituacionFamiliar", s => s.Nombre, s => s.Nombre);
        }
    }
}