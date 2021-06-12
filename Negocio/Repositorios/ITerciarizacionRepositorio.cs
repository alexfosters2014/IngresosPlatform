using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public interface ITerciarizacionRepositorio
    {
        public Task<TerciarizacionDTO> Agregar(TerciarizacionDTO terciarizacionDTO);
        public Task<TerciarizacionDTO> Actualizar(TerciarizacionDTO terciarizacionDTO);
        public Task<TerciarizacionDTO> Obtener(int tercId);
        public Task<List<TerciarizacionDTO>> ObtenerTodos(VMGeneral vmFecha);
        public Task<List<TerciarizacionDTO>> ObtenerTodosXProveedor(VMGeneral vmGeneral);
        public Task<List<TerciarizacionDTO>> ObtenerTodosXProveedorOperador(VMGeneral vmGeneral);

    }
}
