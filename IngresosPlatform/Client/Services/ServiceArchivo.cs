using Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    public class ServiceArchivo : IServiceArchivo
    {
        private readonly HttpClient httpClient;
        public ServiceArchivo(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<byte[]> DescargarExcel(ReporteIngreso reporteIngreso, string token)
        {
            if (reporteIngreso != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.PostAsJsonAsync("/api/Archivo/Planilla",reporteIngreso);
                response.EnsureSuccessStatusCode();
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                return fileBytes;
            }
            else
            {
                return null;
            }
        }
            public async Task<string> ActualizarArchivo(Stream fileStream, string fileName, string pathAnterior, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var contentUpload = new MultipartFormDataContent();
            contentUpload.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            contentUpload.Add(new StreamContent(fileStream, (int)fileStream.Length), "filePDF", fileName);
            contentUpload.Add(new StringContent(pathAnterior ?? ""), "pathPDFAnterior");

            var response = await httpClient.PostAsync($"/api/Archivo/Actualizar", contentUpload);

            if (response.IsSuccessStatusCode)
            {
                var nuevaPath = await response.Content.ReadAsStringAsync();
                return nuevaPath.ToString();
            }
            else
            {
                return "";
            }
        }

        public async Task<string> Subir(Stream fileStream, string fileName, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var contentUpload = new MultipartFormDataContent();
            contentUpload.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            contentUpload.Add(new StreamContent(fileStream, (int)fileStream.Length), "filePDF", fileName);

            var response = await httpClient.PostAsync("/api/Archivo", contentUpload);

            if (response.IsSuccessStatusCode)
            {
                var nuevaPath = await response.Content.ReadAsStringAsync();
                return nuevaPath.ToString(); 
            }
            else
            {
                return "";
            }
        }
    }
}
