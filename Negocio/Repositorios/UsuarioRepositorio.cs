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
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AplicacionDBContext db;
        private readonly IMapper mapper;

        public UsuarioRepositorio(AplicacionDBContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        public async Task<UsuarioDTO> Actualizar(UsuarioDTO usuarioDTO)
        {
            try
            {
                if (usuarioDTO.Id > 0)
                {
                    Usuario usuarioDB = await db.Usuarios.FindAsync(usuarioDTO.Id);
                    Usuario usuario = mapper.Map<UsuarioDTO, Usuario>(usuarioDTO, usuarioDB);
                    var update = db.Usuarios.Update(usuario);
                    await db.SaveChangesAsync();
                    return mapper.Map<Usuario, UsuarioDTO>(update.Entity);
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

        public async Task<UsuarioDTO> Agregar(UsuarioDTO usuarioDTO)
        {
            try
            {

                string passInicial = Generador.GenerarPassword(15);
                usuarioDTO.UsuarioNombre = usuarioDTO.Proveedor.Rut;
                usuarioDTO.TipoUsuario = SD.TipoUsuario.ProveedorIngPlt.ToString();
                usuarioDTO.Token = Generador.GenerarToken();
                Usuario usuario = mapper.Map<UsuarioDTO, Usuario>(usuarioDTO);
                usuario.Password = Encriptar.GetSHA256(passInicial);
                usuario.Email = usuarioDTO.Proveedor.Email;
                Proveedor proveedorBuscado = await db.Proveedores.FindAsync(usuarioDTO.Proveedor.Id);
                if (proveedorBuscado != null)
                {
                    //guardar en BD
                    db.Entry(proveedorBuscado).State = EntityState.Unchanged;
                    usuario.Proveedor = proveedorBuscado;
                    var addUsuario = await db.Usuarios.AddAsync(usuario);
                    await db.SaveChangesAsync();
                    UsuarioDTO uFinal = mapper.Map<Usuario, UsuarioDTO>(addUsuario.Entity);
                    uFinal.PassInicial = passInicial;
                    return uFinal;
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

        public async Task<int> Eliminar(int usuarioId)
        {
            Usuario usuarioDB = await db.Usuarios.FindAsync(usuarioId);
            if (usuarioDB != null)
            {
                db.Remove(usuarioDB);
                return await db.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<UsuarioDTO> Obtener(int usuarioId)
        {
            try
            {
                UsuarioDTO usuario = mapper.Map<Usuario, UsuarioDTO>(await db.Usuarios.FindAsync(usuarioId));
                return usuario;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<UsuarioDTO>> ObtenerTodos()
        {
            try
            {
                List<UsuarioDTO> usuarios = mapper.Map<List<Usuario>, List<UsuarioDTO>>(db.Usuarios.ToList());
                return usuarios;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
