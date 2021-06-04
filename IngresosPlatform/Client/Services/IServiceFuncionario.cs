using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    interface IServiceFuncionario
    {
        public Task<FuncionarioDTO> Agregar(FuncionarioDTO funcionarioDTO);
        public Task<FuncionarioDTO> Actualizar(FuncionarioDTO funcionarioDTO);
        public Task<FuncionarioDTO> Obtener(int? funcionarioDTO);
        public Task<List<FuncionarioDTO>> ObtenerTodos();
        public Task<int> Eliminar(int funcionarioDTO);
        public Task<List<FuncionarioDTO>> ObtenerTodosSegunProveedor(int? proveedorId);

    }
}
