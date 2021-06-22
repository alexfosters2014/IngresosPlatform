using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    public interface IServiceProveedor
    {
        public Task<ProveedorDTO> AgregarProveedor(ProveedorDTO proveedor, string token);
        public Task<ProveedorDTO> ActualizarProveedor(ProveedorDTO proveedorDTO, string token);
        public Task<ProveedorDTO> ObtenerProveedor(int? proveedorId, string token);
        public Task<List<ProveedorDTO>> ObtenerProveedores(string token);
        public Task<int> EliminarProveedor(int proveedorId, string token);
    }
}
