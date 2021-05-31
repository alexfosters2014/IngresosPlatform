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
    public class ServiceFuncionario : IServiceFuncionario
    {
        private readonly HttpClient httpClient;
        public ServiceFuncionario(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<FuncionarioDTO> Actualizar(FuncionarioDTO funcionarioDTO)
        {
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
        }

        public async Task<FuncionarioDTO> Agregar(FuncionarioDTO funcionarioDTO)
        {
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

        public async Task<int> Eliminar(int funcionarioDTO)
        {
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
        public async Task<FuncionarioDTO> Obtener(int? funcionarioDTO)
        {

            if (funcionarioDTO != null)
            {
                var response = await httpClient.GetAsync($"api/Funcionario/{funcionarioDTO.Value}");
                if (response != null)
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
        public async Task<List<FuncionarioDTO>> ObtenerTodos()
        {
            var response = await httpClient.GetAsync("api/Funcionario/");
            var content = await response.Content.ReadAsStringAsync();
            var funcionario = JsonConvert.DeserializeObject<List<FuncionarioDTO>>(content);
            return funcionario;
        }

        public async Task<List<FuncionarioDTO>> ObtenerTodosSegunProveedor(int? funcionarioDTO)
        {
            if (funcionarioDTO != null)
            {
                var response = await httpClient.GetAsync($"api/Funcionario/FunProv/{funcionarioDTO.Value}");
                var content = await response.Content.ReadAsStringAsync();
                var funcionario = JsonConvert.DeserializeObject<List<FuncionarioDTO>>(content);
                return funcionario;
            }
            else
            {
                return null;
            }
        }
    }
}