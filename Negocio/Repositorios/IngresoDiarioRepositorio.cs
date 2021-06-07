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
    public class IngresoDiarioRepositorio : IIngresoDiarioRepositorio
    {
        private readonly AplicacionDBContext db;
        private readonly IMapper mapper;

        public IngresoDiarioRepositorio(AplicacionDBContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public Task<IngresoDiarioDTO> Actualizar(IngresoDiarioDTO funcionarioDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Agregar(IngresoDTO ingresoDTO)
        {
            try
            {
                Proveedor proveedor = await db.Proveedores.FindAsync(ingresoDTO.Proveedor.Id);

                IngresoDiarioDTO ingDiarioDTO = mapper.Map<IngresoDTO, IngresoDiarioDTO>(ingresoDTO);

                if (ingresoDTO.FechaInicio == ingresoDTO.FechaFin)
                {
                    IngresoDiario ingDiarioBD= mapper.Map<IngresoDiarioDTO, IngresoDiario>(ingDiarioDTO);
                    ingDiarioBD.Proveedor = proveedor;
                    db.Entry(ingDiarioBD.Proveedor).State = EntityState.Unchanged;
                    db.Entry(ingDiarioBD.Funcionario).State = EntityState.Unchanged;

                    await db.IngresosDiarios.AddAsync(ingDiarioBD);
                    return await db.SaveChangesAsync();
                }
                if (ingresoDTO.FechaInicio < ingresoDTO.FechaFin)
                {
                    var diffFechas = ingresoDTO.FechaFin - ingresoDTO.FechaInicio;
                    int cantidadDias = diffFechas.Value.Days;
                    DateTime fechaInicio = ingresoDTO.FechaInicio.Value;
                    IngresoDiario ingDiarioBD = mapper.Map<IngresoDiarioDTO, IngresoDiario>(ingDiarioDTO);
                    
                    for (int i=0; i < cantidadDias; i++)
                    {
                        ingDiarioBD.Proveedor = proveedor;
                        ingDiarioBD.Fecha = fechaInicio.AddDays(i);

                        db.Entry(ingDiarioBD.Proveedor).State = EntityState.Unchanged;
                        db.Entry(ingDiarioBD.Funcionario).State = EntityState.Unchanged;
                        await db.IngresosDiarios.AddAsync(ingDiarioBD);
                    }
                    return await db.SaveChangesAsync();
                }




                var addIngDiario = await db.IngresosDiarios.AddAsync(proveedor);
                await db.SaveChangesAsync();
                return mapper.Map<Proveedor, ProveedorDTO>(addProovedor.Entity);
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public Task<int> Borrar(int ingresoId)
        {
            throw new NotImplementedException();
        }

        public Task<List<IngresoDiarioDTO>> ObtenerTodosActivos()
        {
            throw new NotImplementedException();
        }
    }
}
