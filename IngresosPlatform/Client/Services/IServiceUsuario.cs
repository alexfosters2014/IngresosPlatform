using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    public interface IServiceUsuario
    {
        public Task<UsuarioDTO> Agregar(UsuarioDTO usuarioDTO);
        public Task<UsuarioDTO> Actualizar(UsuarioDTO usuarioDTO);
        public Task<UsuarioDTO> Obtener(int? usuarioId);
        public Task<List<UsuarioDTO>> ObtenerTodos();
        public Task<int> EliminarUsuario(int usuarioId);
        public Task<UsuarioDTO> Login(VMLogin vmLogin);
        public Task<string> EnviarMail(MailMensajeDTO mailMensaje);
    }
}
