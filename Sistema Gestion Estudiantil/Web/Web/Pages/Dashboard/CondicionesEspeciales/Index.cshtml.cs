using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Dashboard.CondicionesEspeciales
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _confg;

        public IList<CondicionesEspecialesResponse> CondicionesEspeciales { get; set; } = new List<CondicionesEspecialesResponse>();
        public IList<EstudianteConCondicionesResponse> EstudiantesConCondiciones { get; set; } = new List<EstudianteConCondicionesResponse>();
        public IList<EstudiantesResponse> Estudiantes { get; set; } = new List<EstudiantesResponse>();

        public IndexModel(IConfiguracion confg)
        {
            _confg = confg;
        }

        public async Task OnGet(string filtro = "General", string IdEstudiante = "")
        {
            try
            {
                ViewData["Filtro"] = filtro;
                ViewData["IdEstudiante"] = IdEstudiante;
                CondicionesEspeciales = new List<CondicionesEspecialesResponse>();

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerEstudiantes");
                using var cliente = new HttpClient();
                var respuesta = await cliente.GetAsync(endPoint);
                respuesta.EnsureSuccessStatusCode();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Estudiantes = JsonSerializer.Deserialize<List<EstudiantesResponse>>(resultado, opciones) ?? new List<EstudiantesResponse>();

                Console.WriteLine($"[DEBUG] Estudiantes cargados: {Estudiantes.Count}");

                if (filtro == "ConLista")
                {
                    endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EstudiantesConCondiciones");
                    respuesta = await cliente.GetAsync(endPoint);
                    respuesta.EnsureSuccessStatusCode();
                    resultado = await respuesta.Content.ReadAsStringAsync();
                    EstudiantesConCondiciones = JsonSerializer.Deserialize<List<EstudianteConCondicionesResponse>>(resultado, opciones) ?? new List<EstudianteConCondicionesResponse>();

                    Console.WriteLine($"[DEBUG] Estudiantes con Condiciones cargados: {EstudiantesConCondiciones.Count}");
                }
                else if (filtro == "PorEstudiante" && !string.IsNullOrEmpty(IdEstudiante))
                {
                    endPoint = string.Format(_confg.ObtenerMetodo("ApiEndPoints", "ObtenerCondicionesPorEstudiante"), IdEstudiante);
                    Console.WriteLine($"[DEBUG] URL generada: {endPoint}");

                    respuesta = await cliente.GetAsync(endPoint);
                    if (!respuesta.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"[ERROR] Fallo al obtener condiciones por estudiante {IdEstudiante}, Status Code: {respuesta.StatusCode}");
                        return;
                    }

                    resultado = await respuesta.Content.ReadAsStringAsync();
                    Console.WriteLine($"[DEBUG] JSON recibido para estudiante {IdEstudiante}: {resultado}");

                    CondicionesEspeciales = JsonSerializer.Deserialize<List<CondicionesEspecialesResponse>>(resultado, opciones) ?? new List<CondicionesEspecialesResponse>();

                    Console.WriteLine($"[DEBUG] Condiciones cargadas para estudiante {IdEstudiante}: {CondicionesEspeciales.Count}");
                }
                else
                {
                    endPoint = _confg.ObtenerMetodo("ApiEndPoints", "ObtenerCondiciones");
                    respuesta = await cliente.GetAsync(endPoint);
                    respuesta.EnsureSuccessStatusCode();
                    resultado = await respuesta.Content.ReadAsStringAsync();
                    CondicionesEspeciales = JsonSerializer.Deserialize<List<CondicionesEspecialesResponse>>(resultado, opciones) ?? new List<CondicionesEspecialesResponse>();

                    Console.WriteLine($"[DEBUG] Total condiciones cargadas: {CondicionesEspeciales.Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Fallo en la carga de datos: {ex.Message}");
                CondicionesEspeciales = new List<CondicionesEspecialesResponse>();
            }
        }

        public async Task<IActionResult> OnPostAsync(CondicionesEspecialesRequest condicionEspecial)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(condicionEspecial.TipoCondicion) || string.IsNullOrWhiteSpace(condicionEspecial.NombreCondicion))
                {
                    ModelState.AddModelError("", "Los campos requeridos no pueden estar vacíos.");
                    return Page();
                }

                // Generar GUID si es nulo
                condicionEspecial.IdCondicion ??= Guid.NewGuid();

                // Convertir GUID a string antes de serializar
                var datos = new
                {
                    IdCondicion = condicionEspecial.IdCondicion.ToString(),
                    IdEstudiante = condicionEspecial.IdEstudiante.ToString(),
                    TipoCondicion = condicionEspecial.TipoCondicion,
                    NombreCondicion = condicionEspecial.NombreCondicion,
                    FechaDiagnostico = condicionEspecial.FechaDiagnostico,
                    Observaciones = condicionEspecial.Observaciones
                };

                string jsonCondicionEspecial = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine($"[DEBUG] JSON enviado:\n{jsonCondicionEspecial}");

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "CrearCondicion");
                using var cliente = new HttpClient();
                var contenido = new StringContent(jsonCondicionEspecial, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PostAsync(endPoint, contenido);
                respuesta.EnsureSuccessStatusCode();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var idCondicion = JsonSerializer.Deserialize<Guid>(resultado);

                Console.WriteLine($"[DEBUG] Condición creada con ID: {idCondicion}");

                return RedirectToPage(new { filtro = "PorEstudiante", IdEstudiante = condicionEspecial.IdEstudiante });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Fallo al crear la condición: {ex.Message}");
                ModelState.AddModelError("", "Error al crear la condición especial.");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEliminarAsync(Guid? IdCondicion)
        {
            if (IdCondicion == null || IdCondicion == Guid.Empty)
            {
                return NotFound();
            }

            string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EliminarCondicion");
            string urlFinal = string.Format(endPoint, IdCondicion);
            Console.WriteLine($"[DEBUG] URL generada para eliminación: {urlFinal}");

            using var cliente = new HttpClient();
            using var solicitud = new HttpRequestMessage(HttpMethod.Delete, urlFinal);

            var respuesta = await cliente.SendAsync(solicitud);
            if (!respuesta.IsSuccessStatusCode)
            {
                string contenidoError = await respuesta.Content.ReadAsStringAsync();
                Console.WriteLine($"[ERROR] Respuesta de eliminación: {contenidoError}");
                return Page();
            }

            Console.WriteLine($"[DEBUG] Condición eliminada con ID: {IdCondicion}");
            return RedirectToPage(new { filtro = "General" });
        }


        public async Task<IActionResult> OnPostEditarAsync(Guid IdCondicion, Guid IdEstudiante, CondicionesEspecialesRequest condicionEspecial)
        {
            try
            {
                if (IdCondicion == Guid.Empty || IdEstudiante == Guid.Empty ||
                    string.IsNullOrWhiteSpace(condicionEspecial.TipoCondicion) ||
                    string.IsNullOrWhiteSpace(condicionEspecial.NombreCondicion))
                {
                    ModelState.AddModelError("", "El ID del estudiante es inválido o los campos requeridos están vacíos.");
                    return Page();
                }

                string endPoint = _confg.ObtenerMetodo("ApiEndPoints", "EditarCondicion");

                var datos = new
                {
                    IdCondicion = IdCondicion,
                    IdEstudiante = IdEstudiante, // Verificar que sea correcto
                    TipoCondicion = condicionEspecial.TipoCondicion,
                    NombreCondicion = condicionEspecial.NombreCondicion,
                    FechaDiagnostico = condicionEspecial.FechaDiagnostico,
                    Observaciones = condicionEspecial.Observaciones
                };

                string jsonCondicionEspecial = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine($"[DEBUG] JSON enviado:\n{jsonCondicionEspecial}");

                using var cliente = new HttpClient();
                var contenido = new StringContent(jsonCondicionEspecial, Encoding.UTF8, "application/json");

                var respuesta = await cliente.PutAsync(string.Format(endPoint, IdCondicion), contenido);
                respuesta.EnsureSuccessStatusCode();

                Console.WriteLine($"[DEBUG] Condición editada con ID: {IdCondicion}");

                return RedirectToPage(new { filtro = "PorEstudiante", IdEstudiante });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Fallo al actualizar la condición: {ex.Message}");
                ModelState.AddModelError("", "Error al actualizar la condición especial.");
                return Page();
            }
        }
    }
}