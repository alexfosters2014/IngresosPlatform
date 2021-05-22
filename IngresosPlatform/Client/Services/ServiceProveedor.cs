using Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public Task<ProveedorDTO> AgregarProveedor(ProveedorDTO proveedor)
        {
            throw new NotImplementedException();
        }

        public Task<ProveedorDTO> ObtenerProveedor(int proveedorId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProveedorDTO>> ObtenerProveedores()
        {
            var response = await httpClient.GetAsync($"api/Proveedor");
            var content = await response.Content.ReadAsStringAsync();
            var proveedores = JsonConvert.DeserializeObject<List<ProveedorDTO>>(content);
            return proveedores;
        }
    }
}
