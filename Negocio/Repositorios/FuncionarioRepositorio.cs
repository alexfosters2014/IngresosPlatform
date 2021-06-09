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
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly AplicacionDBContext db;
        private readonly IMapper mapper;

        public FuncionarioRepositorio(AplicacionDBContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        public async Task<FuncionarioDTO> Actualizar(FuncionarioDTO funcionarioDTO)
        {

            try
            {
                if (funcionarioDTO != null)
                {
                    Funcionario funcionarioDB = await db.Funcionarios.Include(i => i.Proveedor).SingleAsync(f => f.Id == funcionarioDTO.Id);
                    Funcionario funcionario = mapper.Map<FuncionarioDTO, Funcionario>(funcionarioDTO, funcionarioDB);
                    db.Entry(funcionario.Proveedor).State = EntityState.Unchanged;
                    var updateFuncionario = db.Funcionarios.Update(funcionario);
                    
                    await db.SaveChangesAsync();
                    return mapper.Map<Funcionario, FuncionarioDTO>(updateFuncionario.Entity);
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

        public async Task<FuncionarioDTO> Agregar(FuncionarioDTO funcionarioDTO)
        {
            try
            {
                Funcionario funcionario = mapper.Map<FuncionarioDTO, Funcionario>(funcionarioDTO);
                
                Proveedor pro = await db.Proveedores.FindAsync(funcionario.Proveedor.Id);
                db.Entry(pro).State = EntityState.Unchanged;
                funcionario.Proveedor = pro;
                var addFuncionario = await db.Funcionarios.AddAsync(funcionario);
                await db.SaveChangesAsync();
                return mapper.Map<Funcionario, FuncionarioDTO>(addFuncionario.Entity);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<int> Borrar(int funcionarioId)
        {
            Funcionario funcinarioDB = await db.Funcionarios.FindAsync(funcionarioId);
            if (funcionarioId != 0)
            {
                funcinarioDB.Activo = false;
                var updateProveedor = db.Update(funcinarioDB);
                return await db.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<List<FuncionarioDTO>> ObtenerTodosActivos()
        {
            try
            {
                List<FuncionarioDTO> funcionarios = mapper.Map<List<Funcionario>, List<FuncionarioDTO>>
                    (db.Funcionarios.Include(i => i.Proveedor).Where(p => p.Activo == true).ToList());
                return funcionarios;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<FuncionarioDTO>> ObtenerTodosSegunProveedor(int proveedorId)
        {
            try
            {
                List<FuncionarioDTO> funcionarios = mapper.Map<List<Funcionario>, List<FuncionarioDTO>>
                    (db.Funcionarios.Include(i => i.Proveedor)
                    .Where(p => p.Proveedor.Id == proveedorId && p.Activo == true).ToList());
                return funcionarios;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<FuncionarioDTO> ObtenerFuncionario(int funcionarioId)
        {
            try
            {
                FuncionarioDTO funcionario = mapper.Map<Funcionario, FuncionarioDTO>(await db.Funcionarios.Include(i => i.Proveedor).SingleAsync(p => p.Id == funcionarioId));
                return funcionario;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
