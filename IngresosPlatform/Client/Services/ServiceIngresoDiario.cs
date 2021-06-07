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
    public class ServiceIngresoDiario : IServiceIngresoDiario
    {
        private readonly HttpClient httpClient;
        public ServiceIngresoDiario(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<IngresoDiarioDTO> ActualizarMarcacion(IngresoDiarioDTO ingresoDiarioDTO)
        {
            var response = await httpClient.PostAsJsonAsync($"/IngresoDiario/Actualizar",ingresoDiarioDTO);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ingresoDiarioActualizadoDTO = JsonConvert.DeserializeObject<IngresoDiarioDTO>(content);
                return ingresoDiarioActualizadoDTO;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<IngresoDiarioDTO>> ObtenerSinMarcaciones(DateTime? fecha)
        {
            if (fecha != null)
            {
                var response = await httpClient.GetAsync($"/IngresoDiario/sinMarca/{fecha.Value}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ingresosDiariosDTO = JsonConvert.DeserializeObject<List<IngresoDiarioDTO>>(content);
                    return ingresosDiariosDTO;
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



    }
}
