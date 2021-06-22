using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    public interface IServiceTerciarizacion
    {
        public Task<TerciarizacionDTO> Agregar(TerciarizacionDTO tercDTO, string token);
        public Task<TerciarizacionDTO> Actualizar(TerciarizacionDTO tercDTO, string token);
        public Task<TerciarizacionDTO> ObtenerIndividual(int tercId, string token);
        public Task<List<TerciarizacionDTO>> ObtenerTodosXProveedor(VMGeneral vmGeneral, string token);
        public Task<List<TerciarizacionDTO>> ObtenerTodosXProveedorOperador(VMGeneral vmGeneral, string token);
        public Task<List<TerciarizacionDTO>> ObtenerTodos(VMGeneral vmFecha, string token);
    }
}
