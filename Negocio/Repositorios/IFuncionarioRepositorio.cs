using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public interface IFuncionarioRepositorio
    {
        public Task<int> Agregar(FuncionarioDTO funcionarioDTO);
        public Task<int> Borrar(int funcionarioId);
        public Task<FuncionarioDTO> Actualizar(FuncionarioDTO funcionarioDTO);
        public Task<List<FuncionarioDTO>> ObtenerTodos();
    }
}
