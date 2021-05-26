using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    public interface IServiceProveedor
    {
        public Task<ProveedorDTO> AgregarProveedor(ProveedorDTO proveedor);
        public Task<ProveedorDTO> ActualizarProveedor(ProveedorDTO proveedorDTO);
        public Task<ProveedorDTO> ObtenerProveedor(int? proveedorId);
        public Task<List<ProveedorDTO>> ObtenerProveedores();
        public Task<int> EliminarProveedor(int proveedorId);
    }
}
