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
                    Ingreso ingresoDB = await db.Ingresos.FindAsync(ingresoDTO.Id);
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

        public async Task<int> Agregar(List<IngresoDTO> ingresosDTO)
        {
            try
            {
                if (ingresosDTO != null && ingresosDTO.Count > 0)
                {
                    foreach (IngresoDTO ingDTO in ingresosDTO) { 

                    Ingreso ingreso = mapper.Map<IngresoDTO, Ingreso>(ingDTO);

                        //buscar el funcionario y el proveedor unchanged
                        Funcionario buscadoF = null;
                        Proveedor buscadoP = null;
                        buscadoF = await db.Funcionarios.FindAsync(ingreso.Funcionario.Id);
                        buscadoP = await db.Proveedores.FindAsync(ingreso.Proveedor.Id);
                        if (buscadoF == null && buscadoP == null) { return 0; }

                        db.Entry(buscadoF).State = EntityState.Unchanged;
                        db.Entry(buscadoP).State = EntityState.Unchanged;
                        ingreso.Proveedor = buscadoP;
                        ingreso.Funcionario = buscadoF;

                        var addIngreso = await db.Ingresos.AddAsync(ingreso);
                    }
                    return await db.SaveChangesAsync();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<bool> AutorizarIngreso(int ingresoId, string estadoAutorizacion)
        {
            try
            {
                if (ingresoId != 0)
                {
                    Ingreso ingresoDB = await db.Ingresos.FindAsync(ingresoId);
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
                    .Where(ing => ing.EstadoAutorizacion == SD.TipoAutIng.Pendiente.ToString()).ToList());
                return ingresos;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
