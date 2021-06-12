using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    public interface IServiceTerciarizacion
    {
        public Task<TerciarizacionDTO> Agregar(TerciarizacionDTO tercDTO);
        public Task<TerciarizacionDTO> Actualizar(TerciarizacionDTO tercDTO);
        public Task<TerciarizacionDTO> ObtenerIndividual(int tercId);
        public Task<List<TerciarizacionDTO>> ObtenerTodosXProveedor(VMGeneral vmGeneral);
        public Task<List<TerciarizacionDTO>> ObtenerTodosXProveedorOperador(VMGeneral vmGeneral);
        public Task<List<TerciarizacionDTO>> ObtenerTodos(VMGeneral vmFecha);
    }
}
