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
        public Task<string> Agregar(List<IngresoDTO> ingresosDTO);
        public Task<int> Borrar(int ingresoId);
        public Task<IngresoDTO> Actualizar(IngresoDTO ingresoDTO);
        public Task<List<IngresoDTO>> ObtenerPendientes();
        public Task<List<IngresoDTO>> ObtenerNoAutorizadosxProveedor(int proveedorId);
        public Task<List<IngresoDTO>> ObtenerAutorizadosxProveedor(int proveedorId);
        public Task<bool> AutorizarIngreso(int ingresoId,string estadoAutorizacion);
        public Task<List<IngresoDTO>> ObtenerAutorizadosYPendientesxProveedor(int proveedorId);
    }
}
