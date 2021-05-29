using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        public Task<FuncionarioDTO> Actualizar(FuncionarioDTO funcionarioDTO)
        {
            throw new NotImplementedException();
        }

        public Task<int> Agregar(FuncionarioDTO funcionarioDTO)
        {
            throw new NotImplementedException();
        }

        public Task<int> Borrar(int funcionarioId)
        {
            throw new NotImplementedException();
        }

        public Task<List<FuncionarioDTO>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
