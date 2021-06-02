using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

        public async Task<string> ActualizarArchivo(Stream fileStream, string fileName, string pathAnterior)
        {
            var contentUpload = new MultipartFormDataContent();
            contentUpload.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            contentUpload.Add(new StreamContent(fileStream, (int)fileStream.Length), "filePDF", fileName);
            contentUpload.Add(new StringContent(pathAnterior), "pathPDFAnterior");

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

        public async Task<string> Subir(Stream fileStream, string fileName)
        {
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
