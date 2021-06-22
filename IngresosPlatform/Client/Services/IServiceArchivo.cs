using Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    public interface IServiceArchivo
    {
        public Task<string> Subir(Stream fileStream, string fileName, string token);
        public Task<string> ActualizarArchivo(Stream fileStream, string fileName,string urlAnterior, string token);
        public Task<byte[]> DescargarExcel(ReporteIngreso reporteIngreso, string token);
    }
}
