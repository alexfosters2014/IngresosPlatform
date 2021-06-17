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
            var response = await httpClient.PostAsJsonAsync("/api/Ingreso/Actualizar", ingresoDTO);
                var content = await response.Content.ReadAsStringAsync();
                var ingresoActualizado = JsonConvert.DeserializeObject<IngresoDTO>(content);
                return ingresoActualizado;
        }

        public async Task<string> AgregarIngresos(List<IngresoDTO> ingresosDTO)
        {
                var response = await httpClient.PostAsJsonAsync("/api/Ingreso", ingresosDTO);
                var content = await response.Content.ReadAsStringAsync();
                //var ingresosNuevos = JsonConvert.DeserializeObject<string>(content);
                return content;
        }

        public async Task<bool> AutorizarIngreso(int ingresoId, string estadoAutorizacion)
        {
            var response = await httpClient.GetAsync($"/api/Ingreso/estado/{ingresoId}/{estadoAutorizacion}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var ingresoActualizado = JsonConvert.DeserializeObject<bool>(content);

                return ingresoActualizado;
            }
            else
            {
                return false;
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

        public async Task<List<IngresoXProveedorDTO>> ObtenerIngresosPendientes()
        {
            var response = await httpClient.GetAsync("api/Ingreso/");
            var content = await response.Content.ReadAsStringAsync();
            var ingresoss = JsonConvert.DeserializeObject<List<IngresoXProveedorDTO>>(content);
            return ingresoss;
        }

        public async Task<List<IngresoDTO>> ObtenerIngresosNoAutXProveedor(int? proveedorId)
        {
            if (proveedorId != null)
            {
                var response = await httpClient.GetAsync($"api/Ingreso/ingresosNoAutorizadosxProveedor/{proveedorId.Value}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ingresos = JsonConvert.DeserializeObject<List<IngresoDTO>>(content);
                    return ingresos;
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

        public async Task<List<IngresoDTO>> ObtenerIngresosAutXProveedor(int? proveedorId)
        {
            if (proveedorId != null)
            {
                var response = await httpClient.GetAsync($"api/Ingreso/ingresosAutorizadosxProveedor/{proveedorId.Value}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ingresos = JsonConvert.DeserializeObject<List<IngresoDTO>>(content);
                    return ingresos;
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

        public async Task<List<IngresoDTO>> ObtenerIngAutYPendXProveedor(int? proveedorId)
        {
            if (proveedorId != null)
            {
                var response = await httpClient.GetAsync($"api/Ingreso/ingresosAutYPendxProveedor/{proveedorId.Value}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ingresos = JsonConvert.DeserializeObject<List<IngresoDTO>>(content);
                    return ingresos;
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
