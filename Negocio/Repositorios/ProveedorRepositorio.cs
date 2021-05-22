using AccesoDatos.Data;
using AutoMapper;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public class ProveedorRepositorio : IProveedorRepositorio
    {
        private readonly AplicacionDBContext db;
        private readonly IMapper mapper;

        public ProveedorRepositorio(AplicacionDBContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public Task<ProveedorDTO> ActualizarProveedor(int ProveedorId, ProveedorDTO proveedor)
        {
            throw new NotImplementedException();
        }

        public async Task<ProveedorDTO> AgregarProveedor(ProveedorDTO proveedorDTO)
        {
            Proveedor proveedor = mapper.Map<ProveedorDTO, Proveedor>(proveedorDTO);
            var adadProovedor = await db.AddAsync(proveedor);                           
        }

        public Task<ProveedorDTO> ExisteProveedor(string rut)
        {
            throw new NotImplementedException();
        }

        public Task<ProveedorDTO> ObtenerProveedor(int proveedorId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProveedorDTO>> ObtenerProveedores()
        {
            throw new NotImplementedException();
        }
    }
}
