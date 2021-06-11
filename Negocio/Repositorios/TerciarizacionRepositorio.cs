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
    public class TerciarizacionRepositorio : ITerciarizacionRepositorio
    {
        private readonly AplicacionDBContext db;
        private readonly IMapper mapper;

        public TerciarizacionRepositorio(AplicacionDBContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public async Task<TerciarizacionDTO> Actualizar(TerciarizacionDTO terciarizacionDTO)
        {
            try
            {
                if (terciarizacionDTO != null)
                {
                    Terciarizacion terciarizaDB = await db.Terciarizaciones.Include(i => i.Proveedor).SingleAsync(f => f.Id == terciarizacionDTO.Id);
                    Terciarizacion terciariza = mapper.Map<TerciarizacionDTO, Terciarizacion>(terciarizacionDTO, terciarizaDB);
                    db.Entry(terciariza.Proveedor).State = EntityState.Unchanged;
                    var update = db.Terciarizaciones.Update(terciariza);

                    await db.SaveChangesAsync();
                    return mapper.Map<Terciarizacion, TerciarizacionDTO>(update.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<TerciarizacionDTO> Agregar(TerciarizacionDTO terciarizacionDTO)
        {
            try
            {
                Terciarizacion terciarizacion = mapper.Map<TerciarizacionDTO, Terciarizacion>(terciarizacionDTO);

                Proveedor pro = await db.Proveedores.FindAsync(terciarizacion.Proveedor.Id);
                db.Entry(pro).State = EntityState.Unchanged;
                terciarizacion.Proveedor = pro;

                var addTerciarizacion = await db.Terciarizaciones.AddAsync(terciarizacion);
                await db.SaveChangesAsync();
                return mapper.Map<Terciarizacion, TerciarizacionDTO>(addTerciarizacion.Entity);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<TerciarizacionDTO> Obtener(int tercId)
        {
            try
            {
                TerciarizacionDTO terc = mapper.Map<Terciarizacion, TerciarizacionDTO>
                    (await db.Terciarizaciones.Include(i => i.Proveedor).SingleAsync(p => p.Id == tercId));
                return terc;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<TerciarizacionDTO>> ObtenerTodos(VMFecha vmFecha)
        {
            try
            {
                List<TerciarizacionDTO> terciariza = mapper.Map<List<Terciarizacion>, List<TerciarizacionDTO>>
                    (db.Terciarizaciones.Include(i => i.Proveedor)
                    .Where(w => w.Fecha.Date == vmFecha.FechaActual.Date)
                    .OrderBy(o => o.Proveedor.RazonSocial)
                    .ToList() );
                return terciariza;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<TerciarizacionDTO>> ObtenerTodosXProveedor(int proveedorId)
        {
            try
            {
                List<TerciarizacionDTO> terc = mapper.Map<List<Terciarizacion>, List<TerciarizacionDTO >>
                    (db.Terciarizaciones.Include(i => i.Proveedor)
                    .Where(p => p.Proveedor.Id == proveedorId)
                    .OrderByDescending(o => o.Fecha)
                    .Take(24)
                    .ToList());
                return terc;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
