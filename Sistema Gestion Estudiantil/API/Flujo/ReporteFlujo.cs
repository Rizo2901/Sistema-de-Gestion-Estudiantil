using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos.Abstracciones.Modelos;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Drawing;
using System.Text;
using Colors = QuestPDF.Helpers.Colors;
using QuestContainer = QuestPDF.Infrastructure.IContainer;
using QuestDocument = QuestPDF.Fluent.Document;

namespace Flujo
{
    public class ReporteFlujo : IReporteFlujo
    {
        private readonly IReporteDA _reporteDA;

        public ReporteFlujo(IReporteDA reporteDA)
        {
            _reporteDA = reporteDA;
        }

        public async Task<object> GenerarReporteUniversal(ReporteEstudiantilRequest filtros)
        {
            var datos = await _reporteDA.ObtenerDatosReporte(filtros);

            // Calcula EstadoGeneral si aplica
            foreach (var estudiante in datos)
                estudiante.EstadoGeneral = CalcularEstado(estudiante);

            object resultado = datos;

            // Aplica operación avanzada si se solicita
            switch (filtros.Operacion?.ToLower())
            {
                case "conteo":
                    if (!string.IsNullOrEmpty(filtros.CampoAgrupacion))
                    {
                        resultado = datos
                            .GroupBy(e => e.GetType().GetProperty(filtros.CampoAgrupacion)?.GetValue(e, null))
                            .Select(g => new { Categoria = g.Key, Cantidad = g.Count() })
                            .ToList();
                    }
                    else
                    {
                        resultado = new { Total = datos.Count() };
                    }
                    break;
                case "porcentaje_menor_calificacion":
                    double limite = filtros.CalificacionLimite ?? 60;
                    int total = datos.Count();
                    int menores = datos.Count(e => e.Calificacion.HasValue && (double)e.Calificacion.Value < limite);
                    double porcentaje = total > 0 ? (menores * 100.0 / total) : 0;
                    resultado = new { Total = total, Menores = menores, Porcentaje = porcentaje };
                    break;
                case "promedio":
                    double promedio = datos.Any(e => e.Calificacion.HasValue)
                        ? datos.Where(e => e.Calificacion.HasValue).Average(e => (double)e.Calificacion.Value)
                        : 0;
                    resultado = new { Promedio = promedio };
                    break;
                    // Puedes agregar más operaciones aquí
            }

            // Si no se solicita exportar, retorna el resultado como JSON
            if (string.IsNullOrWhiteSpace(filtros.ExportarComo))
                return resultado;

            // Si se solicita exportar, genera el archivo en el formato solicitado
            var extension = filtros.ExportarComo.ToLower();
            byte[] archivo;
            switch (extension)
            {
                case "pdf":
                    archivo = GenerarPDFUniversal(resultado, filtros);
                    break;
                case "excel":
                    archivo = GenerarExcelUniversal(resultado, filtros);
                    break;
                case "csv":
                    archivo = GenerarCSVUniversal(resultado, filtros);
                    break;
                default:
                    throw new NotSupportedException("Formato de exportación no soportado.");
            }
            return archivo;
        }

        private string CalcularEstado(ReporteEstudiantilResponse e)
        {
            if (!string.IsNullOrEmpty(e.Enfermedad) || !string.IsNullOrEmpty(e.CondicionEspecial))
                return "Vigilado";
            if (!string.IsNullOrEmpty(e.Alergia) || !string.IsNullOrEmpty(e.Medicamento))
                return "Estable";
            return "Sin registrar";
        }

        // Helpers universales para exportación (puedes mejorarlos según tus necesidades)
        private byte[] GenerarExcelUniversal(object resultado, ReporteEstudiantilRequest filtros)
        {
            using var wb = new XLWorkbook();
            var hoja = wb.Worksheets.Add("Reporte");

            // Header
            hoja.Cell("A1").Value = "Escuela Carolina Dent Alvarado";
            hoja.Range("A1:B1").Merge().Style
                .Font.SetBold()
                .Font.SetFontSize(16)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Fill.SetBackgroundColor(XLColor.LightGray);

            hoja.Cell("A2").Value = $"Reporte de: {filtros.Operacion ?? "Listado"}";
            hoja.Range("A2:B2").Merge().Style
                .Font.SetItalic()
                .Font.SetFontSize(12)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Fill.SetBackgroundColor(XLColor.LightBlue);

            int fila = 4;
            if (resultado is IEnumerable<object> lista && lista.Any())
            {
                var props = lista.First().GetType().GetProperties();
                for (int i = 0; i < props.Length; i++)
                {
                    hoja.Cell(fila, i + 1).Value = props[i].Name;
                    hoja.Cell(fila, i + 1).Style.Font.SetBold();
                }
                fila++;
                foreach (var item in lista)
                {
                    for (int i = 0; i < props.Length; i++)
                        hoja.Cell(fila, i + 1).Value = props[i].GetValue(item)?.ToString() ?? string.Empty;
                    fila++;
                }
            }
            else if (resultado is not string && resultado is not byte[])
            {
                hoja.Cell(fila, 1).Value = "Resultado";
                hoja.Cell(fila, 2).Value = resultado.ToString();
            }

            hoja.Columns().AdjustToContents();
            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            return stream.ToArray();
        }

        private byte[] GenerarCSVUniversal(object resultado, ReporteEstudiantilRequest filtros)
        {
            var sb = new StringBuilder();
            if (resultado is IEnumerable<object> lista && lista.Any())
            {
                var props = lista.First().GetType().GetProperties();
                sb.AppendLine(string.Join(",", props.Select(p => p.Name)));
                foreach (var item in lista)
                    sb.AppendLine(string.Join(",", props.Select(p => $"\"{p.GetValue(item)}\"")));
            }
            else if (resultado is not string && resultado is not byte[])
            {
                sb.AppendLine("Resultado");
                sb.AppendLine($"\"{resultado}\"");
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private byte[] GenerarPDFUniversal(object resultado, ReporteEstudiantilRequest filtros)
        {
            var fecha = DateTime.Now.ToString("dd/MM/yyyy");
            var colorVino = "#800000";
            var titulo = $"Reporte de: {filtros.Operacion ?? "Listado"}";

            var documento = QuestDocument.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);

                    page.Header().Background(colorVino).Padding(10).Column(col =>
                    {
                        col.Item().Text("Escuela Carolina Dent Alvarado")
                            .FontSize(18).Bold().FontColor(Colors.White).AlignCenter();
                        col.Item().Text(titulo)
                            .FontSize(12).FontColor(Colors.White).AlignCenter();
                        col.Item().Text($"Fecha: {fecha}")
                            .FontSize(10).FontColor(Colors.White).AlignCenter();
                    });

                    page.Content().PaddingTop(20).Element(c =>
                    {
                        if (resultado is IEnumerable<object> lista && lista.Any())
                        {
                            c.Table(tabla =>
                            {
                                var props = lista.First().GetType().GetProperties();
                                tabla.ColumnsDefinition(def =>
                                {
                                    for (int i = 0; i < props.Length; i++)
                                        def.RelativeColumn();
                                });
                                tabla.Header(header =>
                                {
                                    foreach (var p in props)
                                        header.Cell().Element(CellEstilo).Text(p.Name);
                                });
                                foreach (var item in lista)
                                {
                                    foreach (var p in props)
                                        tabla.Cell().Element(CellEstilo).Text(p.GetValue(item)?.ToString() ?? "");
                                }
                            });
                        }
                        else
                        {
                            c.Text(resultado?.ToString() ?? "Sin datos").FontSize(14);
                        }
                    });
                });
            });

            return documento.GeneratePdf();
        }

        private QuestContainer CellEstilo(QuestContainer container)
        {
            return container
                .Padding(5)
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten2);
        }
    }
}