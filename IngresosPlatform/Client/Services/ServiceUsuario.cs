﻿using Modelo;
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
        public Task<UsuarioDTO> Actualizar(UsuarioDTO usuarioDTO)
        {
            throw new NotImplementedException();
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

        public Task<UsuarioDTO> Obtener(int? usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UsuarioDTO>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
