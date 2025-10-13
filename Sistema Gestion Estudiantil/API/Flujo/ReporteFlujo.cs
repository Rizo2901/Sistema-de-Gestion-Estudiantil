using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;
using CsvHelper;
using CsvHelper.Configuration;
using FastReport;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using OfficeOpenXml;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Data;
using System.Globalization;



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
            // 1️⃣ Obtener datos crudos desde DA
            var datos = await _reporteDA.ObtenerDatosReporte(filtros);

            // 2️⃣ Filtrar en memoria (AsEnumerable en lugar de AsQueryable)
            var datosFiltrados = datos.AsEnumerable();

            if (!string.IsNullOrEmpty(filtros.Alergia))
                datosFiltrados = datosFiltrados.Where(x => x.Alergia == filtros.Alergia);

            if (!string.IsNullOrEmpty(filtros.Seccion))
                datosFiltrados = datosFiltrados.Where(x => x.Seccion == filtros.Seccion);

            if (!string.IsNullOrEmpty(filtros.Discapacidad))
                datosFiltrados = datosFiltrados.Where(x => x.Discapacidad == filtros.Discapacidad);

            // 3️⃣ Selección de columnas dinámicas
            var propiedades = typeof(ReporteEstudiantilResponse).GetProperties();
            var columnas = filtros.ColumnasSeleccionadas ?? propiedades.Select(p => p.Name).ToList();

            // 4️⃣ Exportación según el formato
            var exportar = filtros.ExportarComo?.ToLower() ?? "pdf";

            switch (exportar)
            {
                case "pdf":
                    return GenerarPdf(datosFiltrados.ToList(), columnas);

                case "excel":
                    return GenerarExcel(datosFiltrados.ToList(), columnas);

                case "csv":
                    return GenerarCsv(datosFiltrados.ToList(), columnas);

                default:
                    throw new ArgumentException("Formato de exportación no soportado. Solo pdf, excel o csv.");
            }
        }

        // ================= PDF con FastReport
        private byte[] GenerarPdf(List<ReporteEstudiantilResponse> datos, List<string> columnas)
        {
            // Validación defensiva
            if (columnas == null || columnas.Count == 0)
                throw new ArgumentException("No se seleccionaron columnas para el reporte.");

            columnas = columnas
                .Where(c => typeof(ReporteEstudiantilResponse).GetProperty(c) != null)
                .Distinct()
                .ToList();

            if (columnas.Count == 0)
                throw new ArgumentException("No hay columnas válidas para renderizar.");

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    // Encabezado institucional
                    page.Header().Element(header =>
                    {
                        header.Background("#b12b38")
                              .PaddingVertical(10)
                              .PaddingHorizontal(15)
                              .Row(row =>
                              {
                                  row.RelativeColumn().Text("S.G.E Escuela Carolina Dent")
                                      .FontSize(18).FontColor(Colors.White).Bold();

                                  row.ConstantColumn(120).AlignRight().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}")
                                      .FontSize(10).FontColor(Colors.White);
                              });
                    });

                    // Contenido principal
                    page.Content().Element(content =>
                    {
                        content.Column(col =>
                        {
                            // Forzar ancho total de tabla
                            col.Item().Width(540).Table(table =>
                            {
                                // Layout defensivo
                                table.ColumnsDefinition(columnsDef =>
                                {
                                    if (columnas.Count == 1)
                                    {
                                        columnsDef.ConstantColumn(540); // ancho total si hay solo una columna
                                    }
                                    else
                                    {
                                        foreach (var _ in columnas)
                                            columnsDef.RelativeColumn(); // distribución proporcional
                                    }
                                });

                                // Encabezados
                                table.Header(header =>
                                {
                                    foreach (var col in columnas)
                                    {
                                        header.Cell().Background("#eeeeee").Padding(5)
                                            .BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                                            .Text(col).Bold().FontSize(10);
                                    }
                                });

                                // Filas
                                foreach (var item in datos)
                                {
                                    foreach (var col in columnas)
                                    {
                                        var prop = item.GetType().GetProperty(col)
                                            ?? item.GetType().GetProperties()
                                            .FirstOrDefault(p => string.Equals(p.Name, col, StringComparison.OrdinalIgnoreCase));

                                        var valor = prop?.GetValue(item)?.ToString()?.Trim();

                                        if (string.IsNullOrWhiteSpace(valor))
                                            valor = "Sin Asignar";

                                        if (valor.Length > 300)
                                            valor = valor.Substring(0, 300) + "...";

                                        table.Cell().PaddingVertical(5).PaddingHorizontal(3)
                                            .BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                                            .Text(valor).FontSize(10);
                                    }
                                }
                            });

                            // Total de registros
                            col.Item().PaddingTop(15).Text(
                                $"Total de registros: {datos.Count}"
                            ).FontSize(12).Bold().FontColor(Colors.Black);
                        });
                    });

                    // Pie institucional
                    page.Footer().AlignCenter().Text("Reporte generado automáticamente por el sistema S.G.E")
                        .FontSize(9).FontColor(Colors.Grey.Darken2);
                });
            });

            return documento.GeneratePdf();
        }
        // ================= Excel con EPPlus
        private byte[] GenerarExcel(List<ReporteEstudiantilResponse> datos, List<string> columnas)
        {
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Estudiantes");

            // Encabezados
            for (int i = 0; i < columnas.Count; i++)
                ws.Cells[1, i + 1].Value = columnas[i];

            // Datos
            for (int r = 0; r < datos.Count; r++)
            {
                var item = datos[r];
                for (int c = 0; c < columnas.Count; c++)
                {
                    var prop = item.GetType().GetProperty(columnas[c]);
                    ws.Cells[r + 2, c + 1].Value = prop?.GetValue(item);
                }
            }

            return package.GetAsByteArray();
        }

        // ================= CSV con CsvHelper
        private byte[] GenerarCsv(List<ReporteEstudiantilResponse> datos, List<string> columnas)
        {
            using var ms = new MemoryStream();
            using var sw = new StreamWriter(ms);
            using var csv = new CsvWriter(sw, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true
            });

            // Encabezados
            foreach (var col in columnas)
                csv.WriteField(col);
            csv.NextRecord();

            // Datos
            foreach (var item in datos)
            {
                foreach (var col in columnas)
                {
                    var prop = item.GetType().GetProperty(col);
                    csv.WriteField(prop?.GetValue(item));
                }
                csv.NextRecord();
            }

            sw.Flush();
            ms.Position = 0;
            return ms.ToArray();
        }

        // ================= Helper: crear DataTable dinámico
        private DataTable CrearDataTable(List<ReporteEstudiantilResponse> datos, List<string> columnas)
        {
            var dt = new DataTable("Estudiantes");
            var propiedades = typeof(ReporteEstudiantilResponse).GetProperties()
                                .Where(p => columnas.Contains(p.Name))
                                .ToList();

            foreach (var prop in propiedades)
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (var item in datos)
            {
                var row = dt.NewRow();
                foreach (var prop in propiedades)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                dt.Rows.Add(row);
            }

            return dt;
        }

        public async Task<List<ReporteEstudiantilResponse>> ObtenerVistaEstudiantilAsync(ReporteEstudiantilRequest filtros)
        {
            var datos = await _reporteDA.ObtenerDatosReporte(filtros);
            var datosFiltrados = datos.AsEnumerable();

            if (!string.IsNullOrEmpty(filtros.Alergia))
                datosFiltrados = datosFiltrados.Where(x => x.Alergia == filtros.Alergia);

            if (!string.IsNullOrEmpty(filtros.CategoriaAlergia))
                datosFiltrados = datosFiltrados.Where(x => x.CategoriaAlergia == filtros.CategoriaAlergia);

            if (!string.IsNullOrEmpty(filtros.Discapacidad))
                datosFiltrados = datosFiltrados.Where(x => x.Discapacidad == filtros.Discapacidad);

            if (!string.IsNullOrEmpty(filtros.CategoriaDiscapacidad))
                datosFiltrados = datosFiltrados.Where(x => x.CategoriaDiscapacidad == filtros.CategoriaDiscapacidad);

            if (!string.IsNullOrEmpty(filtros.CondicionEspecial))
                datosFiltrados = datosFiltrados.Where(x => x.CondicionEspecial == filtros.CondicionEspecial);

            if (!string.IsNullOrEmpty(filtros.CategoriaCondicionEspecial))
                datosFiltrados = datosFiltrados.Where(x => x.CategoriaCondicionEspecial == filtros.CategoriaCondicionEspecial);

            if (!string.IsNullOrEmpty(filtros.Enfermedad))
                datosFiltrados = datosFiltrados.Where(x => x.Enfermedad == filtros.Enfermedad);

            if (!string.IsNullOrEmpty(filtros.Medicamento))
                datosFiltrados = datosFiltrados.Where(x => x.Medicamento == filtros.Medicamento);

            if (!string.IsNullOrEmpty(filtros.Seccion))
                datosFiltrados = datosFiltrados.Where(x => x.Seccion == filtros.Seccion);

            if (!string.IsNullOrEmpty(filtros.CategoriaSituacionFamiliar))
                datosFiltrados = datosFiltrados.Where(x => x.CategoriaSituacionFamiliar == filtros.CategoriaSituacionFamiliar);

            if (!string.IsNullOrEmpty(filtros.EstadoAsistencia))
                datosFiltrados = datosFiltrados.Where(x => x.EstadoAsistencia == filtros.EstadoAsistencia);

            if (!string.IsNullOrEmpty(filtros.TipoDocumento))
                datosFiltrados = datosFiltrados.Where(x => x.TipoDocumento == filtros.TipoDocumento);

            if (!string.IsNullOrEmpty(filtros.TipoAdaptacion))
                datosFiltrados = datosFiltrados.Where(x => x.TipoAdaptacion == filtros.TipoAdaptacion);

            if (!string.IsNullOrEmpty(filtros.MotivoReunion))
                datosFiltrados = datosFiltrados.Where(x => x.MotivoReunion == filtros.MotivoReunion);

            if (filtros.FechaDesde.HasValue)
                datosFiltrados = datosFiltrados.Where(x =>
                    (x.FechaRegistroSituacionFamiliar ?? x.FechaDiagnosticoCondicion ?? x.FechaAdaptacion ?? x.FechaReunion ?? x.FechaAsistencia ?? x.FechaAccionComportamiento ?? x.FechaAtencionMedica) >= filtros.FechaDesde.Value);

            if (filtros.FechaHasta.HasValue)
                datosFiltrados = datosFiltrados.Where(x =>
                    (x.FechaRegistroSituacionFamiliar ?? x.FechaDiagnosticoCondicion ?? x.FechaAdaptacion ?? x.FechaReunion ?? x.FechaAsistencia ?? x.FechaAccionComportamiento ?? x.FechaAtencionMedica) <= filtros.FechaHasta.Value);

            if (filtros.CalificacionLimite.HasValue)
                datosFiltrados = datosFiltrados.Where(x => x.Calificacion.HasValue && x.Calificacion.Value < (decimal)filtros.CalificacionLimite.Value);

            return datosFiltrados.ToList();
        }
    }
}
