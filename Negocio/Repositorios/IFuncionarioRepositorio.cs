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
        public Task<FuncionarioDTO> Agregar(FuncionarioDTO funcionarioDTO);
        public Task<int> Borrar(int funcionarioId);
        public Task<FuncionarioDTO> Actualizar(FuncionarioDTO funcionarioDTO);
        public Task<List<FuncionarioDTO>> ObtenerTodosActivos();
        public Task<FuncionarioDTO> ObtenerFuncionario(int funcionarioId);
        public Task<List<FuncionarioDTO>> ObtenerTodosSegunProveedor(int proveedorId);
    }
}
