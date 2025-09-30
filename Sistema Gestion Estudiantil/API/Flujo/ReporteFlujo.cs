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
            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    // 🎨 Encabezado institucional ancho completo
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

                    // 📊 Tabla de datos con líneas separadoras
                    page.Content().Element(content =>
                    {
                        content.Column(col =>
                        {
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columnsDef =>
                                {
                                    foreach (var _ in columnas)
                                        columnsDef.RelativeColumn();
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

                                // Filas con línea inferior
                                foreach (var item in datos)
                                {
                                    foreach (var col in columnas)
                                    {
                                        var valor = item.GetType().GetProperty(col)?.GetValue(item)?.ToString() ?? "";

                                        table.Cell().PaddingVertical(5).PaddingHorizontal(3)
                                            .BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                                            .Text(valor).FontSize(10);
                                    }
                                }
                            });

                            // 🧮 Total de registros
                            col.Item().PaddingTop(15).Text(
                                $"Total de registros: {datos.Count}"
                            ).FontSize(12).Bold().FontColor(Colors.Black);
                        });
                    });

                    // 📝 Pie institucional
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
    }
}
