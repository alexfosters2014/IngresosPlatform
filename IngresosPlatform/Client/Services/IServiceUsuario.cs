using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    public interface IServiceUsuario
    {
        public Task<UsuarioDTO> Agregar(UsuarioDTO usuarioDTO, string token);
        public Task<UsuarioDTO> Actualizar(UsuarioDTO usuarioDTO, string token);
        public Task<UsuarioDTO> Obtener(int? usuarioId, string token);
        public Task<List<UsuarioDTO>> ObtenerTodos(string token);
        public Task<int> EliminarUsuario(int usuarioId, string token);
        public Task<UsuarioDTO> Login(VMLogin vmLogin);
        public Task<string> EnviarMail(MailMensajeDTO mailMensaje, string token);
    }
}
