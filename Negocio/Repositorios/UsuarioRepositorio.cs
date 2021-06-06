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
        public async Task<UsuarioDTO> Agregar(UsuarioDTO usuarioDTO)
        {
            try
            {

                string passInicial = Generador.GenerarPassword(15);
                usuarioDTO.Token = Generador.GenerarToken();
                Usuario usuario = mapper.Map<UsuarioDTO, Usuario>(usuarioDTO);
                usuario.Password = Encriptacion.GetSHA256(passInicial);
                Proveedor proveedorBuscado = null;
                if (usuarioDTO.Proveedor != null) {
                    proveedorBuscado = await db.Proveedores.FindAsync(usuarioDTO.Proveedor.Id);
                }
                if (proveedorBuscado != null && usuarioDTO.TipoUsuario == SD.TipoUsuario.ProveedorIngPlt.ToString())
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
                else if (usuarioDTO.TipoUsuario != SD.TipoUsuario.ProveedorIngPlt.ToString())
                {
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
                UsuarioDTO usuario = mapper.Map<Usuario, UsuarioDTO>(await db.Usuarios.Include(i => i.Proveedor)
                                                                            .SingleAsync(s => s.Id == usuarioId));
                return usuario;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<UsuarioDTO> Login(VMLogin vmLogin)
        {
            try
            {
                vmLogin.Password = Encriptacion.GetSHA256(vmLogin.Password);
                UsuarioDTO usuario = mapper.Map<Usuario, UsuarioDTO>(await db.Usuarios.Include(i => i.Proveedor)
                                                                    .SingleAsync(u => u.UsuarioNombre == vmLogin.Usuario &&
                                                                                      u.Password == vmLogin.Password));
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
                string operador = SD.TipoUsuario.OperadorIngPlt.ToString();
                string portero = SD.TipoUsuario.PorteriaIngPlt.ToString();
                List<UsuarioDTO> usuarios =
                    mapper.Map<List<Usuario>, List<UsuarioDTO>>(db.Usuarios
                    .Where(us => us.TipoUsuario == operador || us.TipoUsuario == portero).ToList());
                return usuarios;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
