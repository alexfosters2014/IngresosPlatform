using AccesoDatos.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public ProveedorRepositorio(AplicacionDBContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        public async Task<ProveedorDTO> ActualizarProveedor(int proveedorId, ProveedorDTO proveedorDTO)
        {
            try
            {
                if (proveedorId == proveedorDTO.Id)
                {
                    Proveedor proveedorDB = await db.Proveedores.FindAsync(proveedorId);
                    Proveedor proveedor = mapper.Map<ProveedorDTO, Proveedor>(proveedorDTO, proveedorDB);
                    proveedor.Activo = true;
                    var updateProveedor = db.Proveedores.Update(proveedor);
                    await db.SaveChangesAsync();
                    return mapper.Map<Proveedor, ProveedorDTO>(updateProveedor.Entity);
                }
                else
                {
                    return null;
                }
            }catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ProveedorDTO> AgregarProveedor(ProveedorDTO proveedorDTO)
        {
            try { 
            Proveedor proveedor = mapper.Map<ProveedorDTO, Proveedor>(proveedorDTO);
                proveedor.Activo = true;
            var addProovedor = await db.Proveedores.AddAsync(proveedor);
            await db.SaveChangesAsync();
            return mapper.Map<Proveedor, ProveedorDTO>(addProovedor.Entity);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<int> EliminarProveedor(int proveedorId)
        {
            Proveedor proveedorDB = await db.Proveedores.FindAsync(proveedorId);
            if (proveedorDB != null)
            {
                proveedorDB.Activo = false;
                var updateProveedor = db.Update(proveedorDB);
                 await db.SaveChangesAsync();
                var usuario = db.Usuarios.FirstOrDefault(us => us.Proveedor.Id == updateProveedor.Entity.Id);
                if (usuario != null)
                {
                    return usuario.Id;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public async Task<ProveedorDTO> ExisteProveedor(string rut)
        {
            try
            {
                ProveedorDTO proveedor = mapper.Map<Proveedor, ProveedorDTO>(await db.Proveedores.FirstOrDefaultAsync(p => p.Rut.ToLower() == rut.ToLower()));
                return proveedor;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ProveedorDTO> ObtenerProveedor(int proveedorId)
        {
            try
            {
                ProveedorDTO proveedor = mapper.Map<Proveedor, ProveedorDTO>(await db.Proveedores.FirstOrDefaultAsync(p => p.Id == proveedorId));
                return proveedor;
            }catch(Exception e)
            {
                return null;
            }
        }

        public async Task<List<ProveedorDTO>> ObtenerProveedores()
        {
            try
            {
                List<ProveedorDTO> proveedores = mapper.Map <List<Proveedor>, List<ProveedorDTO>>(db.Proveedores.ToList());
                return proveedores;
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}
