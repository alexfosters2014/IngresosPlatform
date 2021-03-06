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
            var response = await httpClient.PostAsJsonAsync("/api/IngresoDiario/Actualizar",ingresoDiarioDTO);
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

        public async Task<VMGeneral> ObtenerFechaAPI()
        {
            var response = await httpClient.GetAsync("/api/IngresoDiario/FechaActual");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var fch = JsonConvert.DeserializeObject<VMGeneral>(content);
                return fch;
            }
            else {
                return new VMGeneral() { FechaActual = DateTime.Today.AddYears(5).Date };
            }
        }

        public async Task<List<IngresoDiarioxProveedor>> ObtenerReporteHorariosEfectivos(VMGeneral vmGeneral)
        {
            if (vmGeneral != null)
            {
                var response = await httpClient.PostAsJsonAsync("/api/IngresoDiario/ReporteHorariosEfectivos", vmGeneral);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ingDiariosXPRoveedorDTO = JsonConvert.DeserializeObject<List<IngresoDiarioxProveedor>>(content);
                    return ingDiariosXPRoveedorDTO;
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
        public async Task<List<IngresoDiarioxProveedor>> ObtenerReporteHorariosPlanificados(VMGeneral vmGeneral)
            {
                if (vmGeneral != null)
                {
                    var response = await httpClient.PostAsJsonAsync("/api/IngresoDiario/ReporteHorariosPlanificados", vmGeneral);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var ingDiariosXPRoveedorDTO = JsonConvert.DeserializeObject<List<IngresoDiarioxProveedor>>(content);
                        return ingDiariosXPRoveedorDTO;
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

        public async Task<List<IngresoDiarioxProveedor>> ObtenerSinMarcaciones(VMGeneral fechaActual)
        {
            if (fechaActual != null)
            {
                var response = await httpClient.PostAsJsonAsync("/api/IngresoDiario/sinMarca", fechaActual);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ingDiariosXPRoveedorDTO = JsonConvert.DeserializeObject<List<IngresoDiarioxProveedor>>(content);
                    return ingDiariosXPRoveedorDTO;
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
