using Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public Task<ProveedorDTO> ActualizarProveedor(int proveedorId, ProveedorDTO proveedorDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ProveedorDTO> AgregarProveedor(ProveedorDTO proveedor)
        {
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

        public async Task<ProveedorDTO> ObtenerProveedor(int? proveedorId)
        {
            if( proveedorId != null) {
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

        public async Task<List<ProveedorDTO>> ObtenerProveedores()
        {
            var response = await httpClient.GetAsync("api/Proveedor/");
            var content = await response.Content.ReadAsStringAsync();
            var proveedores = JsonConvert.DeserializeObject<List<ProveedorDTO>>(content);
            return proveedores;
        }
    }
}
