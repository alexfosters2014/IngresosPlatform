using Comun;
using Modelo;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatformWebAPI
{
    public class Utils
    {
        public static ExcelPackage createExcel(ReporteIngreso reporte)
        {
            List<IngresoDiarioxProveedor> ingresosDiarios = reporte.IngresosXProveedores;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage();

            package.Workbook.Properties.Title = "Reporte";

            var worksheet = package.Workbook.Worksheets.Add("Reporte");

            int fila = 1;
            ProveedorDTO proveedor;

            foreach (var ingProv in ingresosDiarios)
            {
                proveedor = ingProv.IngresosDiarios.First().Proveedor;
                //Cabecera
                worksheet.Cells[fila, 1].Value = $"{proveedor.RazonSocial} ({proveedor.NombreFantasia})";
                worksheet.Cells[fila, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 1].Style.Font.Bold = true;
                worksheet.Cells[fila, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells[fila, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255,255,0));

                fila++;

                worksheet.Cells[fila, 1].Value = "Fecha";
                worksheet.Cells[fila, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 1].Style.Border.Top.Color.SetColor(Color.Black);
                worksheet.Cells[fila, 1].Style.Font.Bold = true;

                worksheet.Cells[fila, 2].Value = "Cedula";
                worksheet.Cells[fila, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 2].Style.Border.Top.Color.SetColor(Color.Black);
                worksheet.Cells[fila, 2].Style.Font.Bold = true;

                worksheet.Cells[fila, 3].Value = "Funcionario";
                worksheet.Cells[fila, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 3].Style.Border.Top.Color.SetColor(Color.Black);
                worksheet.Cells[fila, 3].Style.Font.Bold = true;

                worksheet.Cells[fila, 4].Value = "Hora Entrada";
                worksheet.Cells[fila, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 4].Style.Border.Top.Color.SetColor(Color.Black);
                worksheet.Cells[fila, 4].Style.Font.Bold = true;

                worksheet.Cells[fila, 5].Value = "Hora Salida";
                worksheet.Cells[fila, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[fila, 5].Style.Border.Top.Color.SetColor(Color.Black);
                worksheet.Cells[fila, 5].Style.Font.Bold = true;

                fila++;
                foreach (var ing in ingProv.IngresosDiarios)
                {
                    worksheet.Cells[fila, 1].Value = ing.Fecha.ToString("dd/MM/yyyy");
                    worksheet.Cells[fila, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 1].Style.Border.Top.Color.SetColor(Color.Black);

                    worksheet.Cells[fila, 2].Value = ing.Funcionario.Cedula;
                    worksheet.Cells[fila, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 2].Style.Border.Top.Color.SetColor(Color.Black);

                    worksheet.Cells[fila, 3].Value = ing.Funcionario.Nombre;
                    worksheet.Cells[fila, 3].Value = "Funcionario";
                    worksheet.Cells[fila, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 3].Style.Border.Top.Color.SetColor(Color.Black);

                    if (reporte.TipoFecha == SD.TipoFecha.EFECTIVA.ToString())
                    {
                        worksheet.Cells[fila, 4].Value = (ing.EntradaEfectiva != null ? ing.EntradaEfectiva.Value.ToString("HH:mm") : "sin marca");
                        worksheet.Cells[fila, 5].Value = (ing.SalidaEfectiva != null ? ing.SalidaEfectiva.Value.ToString("HH:mm") : "sin marca");
                    }
                    else if (reporte.TipoFecha == SD.TipoFecha.PLANIFICADA.ToString())
                    {
                        worksheet.Cells[fila, 4].Value = ing.EntradaPlanificada.ToString("HH:mm");
                        worksheet.Cells[fila, 5].Value = ing.SalidaPlanificada.ToString("HH:mm");
                    }
                    worksheet.Cells[fila, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 4].Style.Border.Top.Color.SetColor(Color.Black);

                    worksheet.Cells[fila, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[fila, 5].Style.Border.Top.Color.SetColor(Color.Black);

                    fila++;
                }
                fila += 2;
            }

            worksheet.Cells.AutoFitColumns();
            return package;
        }
    }
}
