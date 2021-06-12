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
    public class ServiceTerciarizacion : IServiceTerciarizacion
    {
        private readonly HttpClient httpClient;
        public ServiceTerciarizacion(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<TerciarizacionDTO> Actualizar(TerciarizacionDTO tercDTO)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Terciarizacion", tercDTO);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Actualizado = JsonConvert.DeserializeObject<TerciarizacionDTO>(content);
                return Actualizado;
            }
            else
            {
                return null;
            }
        }

        public async Task<TerciarizacionDTO> Agregar(TerciarizacionDTO tercDTO)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Terciarizacion", tercDTO);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var nuevo = JsonConvert.DeserializeObject<TerciarizacionDTO>(content);
                return nuevo;
            }
            else
            {
                return null;
            }
        }

        public async Task<TerciarizacionDTO> ObtenerIndividual(int tercId)
        {
            var response = await httpClient.GetAsync($"/api/Terciarizacion/{tercId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var nuevo = JsonConvert.DeserializeObject<TerciarizacionDTO>(content);
                return nuevo;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<TerciarizacionDTO>> ObtenerTodos(VMGeneral vmFecha)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Terciarizacion/ObtenerTodos",vmFecha);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todos = JsonConvert.DeserializeObject<List<TerciarizacionDTO>>(content);
                return todos;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<TerciarizacionDTO>> ObtenerTodosXProveedor(VMGeneral vmGeneral)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Terciarizacion/ObtenerTodosXProveedor", vmGeneral);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todos = JsonConvert.DeserializeObject<List<TerciarizacionDTO>>(content);
                return todos;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<TerciarizacionDTO>> ObtenerTodosXProveedorOperador(VMGeneral vmGeneral)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Terciarizacion/ObtenerTodosXProveedorOperador", vmGeneral);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todos = JsonConvert.DeserializeObject<List<TerciarizacionDTO>>(content);
                return todos;
            }
            else
            {
                return null;
            }
        }
    }
}