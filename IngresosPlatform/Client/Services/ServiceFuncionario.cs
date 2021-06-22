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
    public class ServiceFuncionario : IServiceFuncionario
    {
        private readonly HttpClient httpClient;
        public ServiceFuncionario(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<FuncionarioDTO> Actualizar(FuncionarioDTO funcionarioDTO, string token)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.PostAsJsonAsync("/api/Funcionario", funcionarioDTO);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var funciionarioActualizado = JsonConvert.DeserializeObject<FuncionarioDTO>(content);

                    return funciionarioActualizado;
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                Console.WriteLine($"Error recibido: {ex.Message}");
                return null;
            }

        }

        public async Task<FuncionarioDTO> Agregar(FuncionarioDTO funcionarioDTO, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsJsonAsync("/api/Funcionario", funcionarioDTO);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var funcionarioNuevo = JsonConvert.DeserializeObject<FuncionarioDTO>(content);
                return funcionarioNuevo;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> Eliminar(int funcionarioDTO, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.DeleteAsync($"/api/Funcionario/{funcionarioDTO}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                int resultado = JsonConvert.DeserializeObject<int>(content);
                return resultado;
            }
            else
            {
                return 0;
            }
        }
        public async Task<FuncionarioDTO> Obtener(int? funcionarioDTO, string token)
        {

            if (funcionarioDTO != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync($"api/Funcionario/{funcionarioDTO.Value}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var funcionario = JsonConvert.DeserializeObject<FuncionarioDTO>(content);
                    return funcionario;
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
        public async Task<List<FuncionarioDTO>> ObtenerTodos(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("api/Funcionario/");
            var content = await response.Content.ReadAsStringAsync();
            var funcionarios = JsonConvert.DeserializeObject<List<FuncionarioDTO>>(content);
            return funcionarios;
        }

        public async Task<List<FuncionarioDTO>> ObtenerTodosSegunProveedor(int? proveedorId, string token)
        {
            if (proveedorId != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync($"api/Funcionario/FunProv/{proveedorId.Value}");
                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    var funcionarios = JsonConvert.DeserializeObject<List<FuncionarioDTO>>(content);
                    return funcionarios;
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