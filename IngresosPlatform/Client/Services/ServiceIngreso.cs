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
    public class ServiceIngreso : IServiceIngreso
    {

        private readonly HttpClient httpClient;
        public ServiceIngreso(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<IngresoDTO> ActualizarIngreso(IngresoDTO ingresoDTO)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Ingreso", ingresoDTO);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var ingresoActualizado = JsonConvert.DeserializeObject<IngresoDTO>(content);

                return ingresoActualizado;
            }
            else
            {
                return null;
            }
        }

        public async Task<IngresoDTO> AgregarIngreso(IngresoDTO ingresoDTO)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Ingreso", ingresoDTO);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var ingresoNuevo = JsonConvert.DeserializeObject<IngresoDTO>(content);
                return ingresoNuevo;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> EliminarIngreso(int IngresoId)
        {
            var response = await httpClient.DeleteAsync($"/api/Ingreso/{IngresoId}");

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IngresoDTO> ObtenerIngreso(int? ingresoId)
        {
            if (ingresoId != null)
            {
                var response = await httpClient.GetAsync($"api/Ingreso/{ingresoId.Value}");
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ingreso = JsonConvert.DeserializeObject<IngresoDTO>(content);
                    return ingreso;
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

        public async Task<List<IngresoXProveedorDTO>> ObtenerIngresos()
        {
            var response = await httpClient.GetAsync("api/Ingreso/");
            var content = await response.Content.ReadAsStringAsync();
            var usuarios = JsonConvert.DeserializeObject<List<IngresoXProveedorDTO>>(content);
            return usuarios;
        }
    }
}
