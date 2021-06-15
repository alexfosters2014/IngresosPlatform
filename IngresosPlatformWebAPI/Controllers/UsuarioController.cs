using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelo;
using Comun;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IngresosPlatformWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IMailRepositorio mailRepositorio;
        private readonly IOptions<ConfigMail> config;

        public UsuarioController(IUsuarioRepositorio _usuarioRepositorio, IMailRepositorio _mailRepositorio, IOptions<ConfigMail> _config)
        {
            usuarioRepositorio = _usuarioRepositorio;
            mailRepositorio = _mailRepositorio;
            config = _config;
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todosUsuarios = await usuarioRepositorio.ObtenerTodos();
            return Ok(todosUsuarios);
        }
        [HttpPost("EnviarMail")]
        public async Task<IActionResult> EnviarMAil([FromBody] MailMensajeDTO mailMensaje)
        {
            MailDTO mail = await mailRepositorio.CargarConfigMail(config.Value);
            if (await mail.EnvioAutentificacionProveedor(mailMensaje.EnviarA, mailMensaje.MensajeAEnviar))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            UsuarioDTO usuarioNuevo = await usuarioRepositorio.Agregar(usuarioDTO);
            if (usuarioNuevo == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Titulo = "",
                    ErrorMensaje = "Proveedor ya existe o no puedo crearse",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            MailDTO mail = await mailRepositorio.CargarConfigMail(config.Value);
            string mensaje;
            if (usuarioNuevo.TipoUsuario == SD.TipoUsuario.ProveedorIngPlt.ToString())
            {
                mensaje = $"Bienvenidos a ProIngreso. <br/>" +
                          $"Su usuario es su RUT: {usuarioNuevo.Proveedor.Rut} <br/>" +
                          $"Su contraseña inicial es: {usuarioNuevo.PassInicial} <br/>" +
                          "Debe acceder al siguiente link: https://proingreso.azurewebsites.net/la , ingresar su usuario, copiar y pegar su contraseña inicial e ingresar <br/>" +
                          "Recuerde que al autentificarse por primera vez deberá cambiar la contraseña por una personal";
            }
            else
            {
                   mensaje = $"Bienvenidos a ProIngreso. <br/>" +
                             $"Su usuario es: {usuarioNuevo.UsuarioNombre} <br/>" +
                             $"Su contraseña inicial es: {usuarioNuevo.PassInicial} <br/>" +
                             "Debe acceder al siguiente link: https://proingreso.azurewebsites.net/ , ingresar su usuario, copiar y pegar su contraseña inicial e ingresar <br/>" +
                             "Recuerde que al autentificarse por primera vez deberá cambiar la contraseña por una personal";
            }
            if (await mail.EnvioAutentificacionProveedor(usuarioNuevo.Email, mensaje)) {
                return Ok(usuarioNuevo);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    Titulo = "",
                    ErrorMensaje = "Proveedor ya existe o no puedo crearse",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
        }

        [HttpPost("Actualizar")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            UsuarioDTO usuarioActualizar = await usuarioRepositorio.Actualizar(usuarioDTO);
            if (usuarioActualizar == null)
            {
                return BadRequest();
            }
            usuarioActualizar.PassInicial = "";
            return Ok(usuarioActualizar);
        }

        [HttpDelete("{usuarioId}")]
        public async Task<IActionResult> Delete(int? usuarioId)
        {
            if (usuarioId != null)
            {
                var resultado = await usuarioRepositorio.Eliminar(usuarioId.Value);
                if (resultado == 0)
                {
                    return BadRequest(null); ;
                }
                return Ok();
            }
            else
            {
                return BadRequest(null);
            }

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] VMLogin vmLogin)
        {
            if (vmLogin != null)
            { 
            var resultado = await usuarioRepositorio.Login(vmLogin);
            if (resultado == null)
                {
                    return BadRequest(null); ;
                }
            return Ok(resultado);
            }
            else
                {
                    return BadRequest(null);
                }
        }
    }
}
