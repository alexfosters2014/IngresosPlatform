using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    interface IServiceIngreso
    {
        public Task<string> AgregarIngresos(List<IngresoDTO> ingresosDTO);
        public Task<IngresoDTO> ActualizarIngreso(IngresoDTO ingresoDTO);
        public Task<IngresoDTO> ObtenerIngreso(int? ingresoId);
        public Task<List<IngresoXProveedorDTO>> ObtenerIngresosPendientes();
        public Task<List<IngresoDTO>> ObtenerIngresosNoAutXProveedor(int? proveedorId);
        public Task<List<IngresoDTO>> ObtenerIngresosAutXProveedor(int? proveedorId);
        public Task<int> EliminarIngreso(int IngresoId);
        public Task<bool> AutorizarIngreso(int ingresoId, string estadoAutorizacion);
    }
}
