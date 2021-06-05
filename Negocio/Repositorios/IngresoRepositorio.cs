using AccesoDatos.Data;
using AutoMapper;
using Comun;
using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public class IngresoRepositorio : IIngresoRepositorio
    {
        private readonly AplicacionDBContext db;
        private readonly IMapper mapper;

        public IngresoRepositorio(AplicacionDBContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        public async Task<IngresoDTO> Actualizar(IngresoDTO ingresoDTO)
        {
            try
            {
                if (ingresoDTO != null )
                {
                    Ingreso ingresoDB = await db.Ingresos.Include(i => i.Proveedor).SingleAsync(s => s.Id == ingresoDTO.Id);
                    Ingreso ingreso = mapper.Map<IngresoDTO, Ingreso>(ingresoDTO, ingresoDB);
                    var updateIngreso = db.Ingresos.Update(ingreso);
                    await db.SaveChangesAsync();
                    return mapper.Map<Ingreso, IngresoDTO>(updateIngreso.Entity);
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

        public async Task<string> Agregar(List<IngresoDTO> ingresosDTO)
        {
            int contador = 0;
            Ingreso ingreso = null;
            List<Ingreso> ingresos = null;
            try
            {

                //db.Ingresos.AsNoTracking().ToList();
                if (ingresosDTO != null && ingresosDTO.Count > 0)
                {
                    ingresos = mapper.Map<List<IngresoDTO>, List<Ingreso>>(ingresosDTO);
                    Proveedor buscarProveedor = await db.Proveedores.FindAsync(ingresos.First().Proveedor.Id);
                    foreach (Ingreso ing in ingresos)
                    {
                        ing.Proveedor = buscarProveedor;
                        db.Entry(ing.Proveedor).State = EntityState.Unchanged;
                        db.Entry(ing.Funcionario).State = EntityState.Unchanged;

                        if (await db.IngresosDiarios.AnyAsync(t => t.Fecha >= ing.FechaInicio
                         && t.Fecha < ing.FechaFin.AddDays(1) && t.Funcionario.Id == ing.Funcionario.Id))
                        {
                            ing.EstadoAutorizacion = SD.TipoAutIng.NOAUTORIZADO.ToString();
                            ing.Comentarios = "Parte del rango de fechas solicitadas ya estan ingresadas. Verifique";
                        }
                        else
                        {
                            ing.EstadoAutorizacion = SD.TipoAutIng.PENDIENTE.ToString();
                            contador++;
                        }
                    }
                    await db.Ingresos.AddRangeAsync(ingresos);
                    await db.SaveChangesAsync();


                    if (ingresosDTO.Count == contador) {
                        return SD.IngresosReturn.OK.ToString();
                    }
                    else {
                        return SD.IngresosReturn.REVISAR.ToString();
                    } 
                }
                else
                {
                    return SD.IngresosReturn.ERROR.ToString();
                }
            }
            catch (Exception e)
            {
                return SD.IngresosReturn.ERROR.ToString();
            }
        }

        public async Task<bool> AutorizarIngreso(int ingresoId, string estadoAutorizacion)
        {
            try
            {
                if (ingresoId != 0)
                {
                    Ingreso ingresoDB = await db.Ingresos.Include(i => i.Proveedor).SingleAsync(i => i.Id == ingresoId);
                    ingresoDB.EstadoAutorizacion = estadoAutorizacion;
                    var updateIngreso = db.Ingresos.Update(ingresoDB);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<int> Borrar(int ingresoId)
        {
            Ingreso IngresoDB = await db.Ingresos.FindAsync(ingresoId);
            if (IngresoDB != null)
            {
                db.Remove(IngresoDB);
                return await db.SaveChangesAsync();
            }
                else
                {
                    return 0;
                }
        }

        public async Task<List<IngresoDTO>> ObtenerPendientes()
        {
            try
            {
                List<IngresoDTO> ingresos =
                    mapper.Map<List<Ingreso>, List<IngresoDTO>>(db.Ingresos
                    .Include(i => i.Proveedor)
                    .Where(ing => ing.EstadoAutorizacion == SD.TipoAutIng.PENDIENTE.ToString()).ToList());
                return ingresos;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<IngresoDTO>> ObtenerNoAutorizadosxProveedor(int proveedorId)
        {
            try
            {
                List<IngresoDTO> ingresos =
                    mapper.Map<List<Ingreso>, List<IngresoDTO>>(db.Ingresos
                    .Include(i => i.Proveedor)
                    .Include(i => i.Funcionario)
                    .Where(ing => ing.EstadoAutorizacion == SD.TipoAutIng.NOAUTORIZADO.ToString()
                            && ing.Proveedor.Id == proveedorId).ToList());
                return ingresos;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<IngresoDTO>> ObtenerAutorizadosxProveedor(int proveedorId)
        {
            try
            {
                List<IngresoDTO> ingresos =
                    mapper.Map<List<Ingreso>, List<IngresoDTO>>(db.Ingresos
                    .Include(i => i.Proveedor)
                    .Include(i => i.Funcionario)
                    .Where(ing => ing.EstadoAutorizacion == SD.TipoAutIng.AUTORIZADO.ToString()
                            && ing.Proveedor.Id == proveedorId).ToList());
                return ingresos;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }


}
