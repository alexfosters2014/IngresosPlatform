using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public interface IIngresoRepositorio
    {
        public Task<int> Agregar(List<IngresoDTO> ingresosDTO);
        public Task<int> Borrar(int ingresoId);
        public Task<IngresoDTO> Actualizar(IngresoDTO ingresoDTO);
        public Task<List<IngresoDTO>> ObtenerPendientes();

        public Task<bool> AutorizarIngreso(int ingresoId,string estadoAutorizacion);
    }
}
