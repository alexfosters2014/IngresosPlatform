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
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace IngresosPlatformWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IMailRepositorio mailRepositorio;
        private readonly IOptions<ConfigMail> config;
        private readonly IConfiguration configuration;

        public UsuarioController(IUsuarioRepositorio _usuarioRepositorio, 
                                    IMailRepositorio _mailRepositorio, IOptions<ConfigMail> _config,IConfiguration _configuration)
        {
            usuarioRepositorio = _usuarioRepositorio;
            mailRepositorio = _mailRepositorio;
            config = _config;
            configuration = _configuration;
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
        [AllowAnonymous]
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

                // Leemos el secret_key desde nuestro appseting
                var secretKey = configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                // Creamos los claims (pertenencias, características) del usuario
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, resultado.Id.ToString()),
                    new Claim(ClaimTypes.Email, resultado.Email)
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    // Nuestro token va a durar un día
                    Expires = DateTime.UtcNow.AddDays(1),
                    // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                var token = tokenHandler.WriteToken(createdToken);

                resultado.Token = token;

                return Ok(resultado);
            }
            else
                {
                    return BadRequest(null);
                }
        }
    }
}
