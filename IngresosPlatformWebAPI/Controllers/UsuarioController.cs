using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelo;
using Comun;
using Microsoft.AspNetCore.Http;

namespace IngresosPlatformWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IMailRepositorio mailRepositorio;

        public UsuarioController(IUsuarioRepositorio _usuarioRepositorio, IMailRepositorio _mailRepositorio)
        {
            usuarioRepositorio = _usuarioRepositorio;
            mailRepositorio = _mailRepositorio;
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
            MailDTO mail = await mailRepositorio.CargarConfigMail();
            string mensaje= $"Bienvenidos a Ingresos Platform. Su usuario es su RUT y la contraseña inicial es: {usuarioNuevo.PassInicial}, la cual deberá cambiar una vez autentificado al sistema";
            if (await mail.EnvioAutentificacionProveedor(usuarioNuevo.Email, mensaje)){
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
    }
}
