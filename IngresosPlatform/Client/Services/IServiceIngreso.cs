using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    interface IServiceIngreso
    {
        public Task<string> AgregarIngresos(List<IngresoDTO> ingresosDTO, string token);
        public Task<IngresoDTO> ActualizarIngreso(IngresoDTO ingresoDTO, string token);
        public Task<IngresoDTO> ObtenerIngreso(int? ingresoId, string token);
        public Task<List<IngresoXProveedorDTO>> ObtenerIngresosPendientes(string token);
        public Task<List<IngresoDTO>> ObtenerIngresosNoAutXProveedor(int? proveedorId, string token);
        public Task<List<IngresoDTO>> ObtenerIngresosAutXProveedor(int? proveedorId, string token);
        public Task<int> EliminarIngreso(int IngresoId, string token);
        public Task<bool> AutorizarIngreso(int ingresoId, string estadoAutorizacion, string token);
        public Task<List<IngresoDTO>> ObtenerIngAutYPendXProveedor(int? proveedorId, string token);
    }
}
