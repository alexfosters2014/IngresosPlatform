using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    interface IServiceFuncionario
    {
        public Task<FuncionarioDTO> Agregar(FuncionarioDTO funcionarioDTO, string token);
        public Task<FuncionarioDTO> Actualizar(FuncionarioDTO funcionarioDTO, string token);
        public Task<FuncionarioDTO> Obtener(int? funcionarioDTO, string token);
        public Task<List<FuncionarioDTO>> ObtenerTodos(string token);
        public Task<int> Eliminar(int funcionarioDTO, string token);
        public Task<List<FuncionarioDTO>> ObtenerTodosSegunProveedor(int? proveedorId, string token);

    }
}
