using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        public Task<UsuarioDTO> Agregar(UsuarioDTO usuarioDTO);
        public Task<UsuarioDTO> Actualizar(UsuarioDTO usuarioDTO);
        public Task<UsuarioDTO> Obtener(int usuarioId);
        public Task<List<UsuarioDTO>> ObtenerTodos();
        public Task<UsuarioDTO> Login(VMLogin vmLogin);
        public Task<int> Eliminar(int usuarioId);
    }
}
