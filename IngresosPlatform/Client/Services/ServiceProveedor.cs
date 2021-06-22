using Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    public class ServiceProveedor : IServiceProveedor
    {
        private readonly HttpClient httpClient;
        public ServiceProveedor(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<ProveedorDTO> ActualizarProveedor(ProveedorDTO proveedorDTO, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsJsonAsync("/api/Proveedor", proveedorDTO);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var proveedorActualizado = JsonConvert.DeserializeObject<ProveedorDTO>(content);
                return proveedorActualizado;
            }
            else
            {
                return null;
            }
        }

        public async Task<ProveedorDTO> AgregarProveedor(ProveedorDTO proveedor, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsJsonAsync("/api/Proveedor",proveedor);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var proveedorNuevo = JsonConvert.DeserializeObject<ProveedorDTO>(content);
                return proveedorNuevo;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> EliminarProveedor(int proveedorId, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.DeleteAsync($"/api/Proveedor/{proveedorId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                int usuarioID = JsonConvert.DeserializeObject<int>(content);
                return usuarioID;
            }
            else
            {
                return 0;
            }
        }

        public async Task<ProveedorDTO> ObtenerProveedor(int? proveedorId,string token)
        {
            if( proveedorId != null) {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync($"api/Proveedor/{proveedorId.Value}");
            if (response != null) { 
            var content = await response.Content.ReadAsStringAsync();
            var proveedor = JsonConvert.DeserializeObject<ProveedorDTO>(content);
            return proveedor;
            }
            else
            {
                return null;
            }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ProveedorDTO>> ObtenerProveedores(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("api/Proveedor/");
            var content = await response.Content.ReadAsStringAsync();
            var proveedores = JsonConvert.DeserializeObject<List<ProveedorDTO>>(content);
            return proveedores;
        }
    }
}
