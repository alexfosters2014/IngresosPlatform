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
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly HttpClient httpClient;
        public ServiceUsuario(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<UsuarioDTO> Actualizar(UsuarioDTO usuarioDTO)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Usuario/Actualizar", usuarioDTO);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuarioActualizado = JsonConvert.DeserializeObject<UsuarioDTO>(content);
                return usuarioActualizado;
            }
            else
            {
                return null;
            }
        }

        public async Task<UsuarioDTO> Agregar(UsuarioDTO usuarioDTO)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Usuario", usuarioDTO);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var usuarioNuevo = JsonConvert.DeserializeObject<UsuarioDTO>(content);
                return usuarioNuevo;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> EliminarUsuario(int usuarioId)
        {
            var response = await httpClient.DeleteAsync($"/api/Usuario/{usuarioId}");

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<UsuarioDTO> Login(VMLogin vmLogin)
        {
            var response = await httpClient.PostAsJsonAsync($"/api/Usuario/Login", vmLogin);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<UsuarioDTO>(content);
                return usuario;
            }
            else
            {
                return null;
            }
        }

        public Task<UsuarioDTO> Obtener(int? usuarioId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UsuarioDTO>> ObtenerTodos()
        {
            var response = await httpClient.GetAsync("/api/Usuario/");
            var content = await response.Content.ReadAsStringAsync();
            var usuarios = JsonConvert.DeserializeObject<List<UsuarioDTO>>(content);
            return usuarios;
        }
    public async Task<string> EnviarMail(MailMensajeDTO mailMensaje)
    {
        var response = await httpClient.PostAsJsonAsync("/api/Usuario/EnviarMail", mailMensaje);

        if (response.IsSuccessStatusCode)
        {
                return "";
            }
        else
        {
                return "No se pudo enviar el mail al proveedor seleccionado"; 
        }
     }
  }
}
