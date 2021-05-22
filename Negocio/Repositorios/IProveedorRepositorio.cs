using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public interface IProveedorRepositorio
    {
        public Task<ProveedorDTO> AgregarProveedor(ProveedorDTO proveedor);
        public Task<ProveedorDTO> ActualizarProveedor(int ProveedorId, ProveedorDTO proveedor);
        public Task<ProveedorDTO> ObtenerProveedor(int proveedorId);
        public Task<List<ProveedorDTO>> ObtenerProveedores();

        public Task<ProveedorDTO>  ExisteProveedor(string rut);

    }
}
