using Microsoft.AspNetCore.Mvc;
using Modelo;
using Negocio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatformWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngresoDiarioController : Controller
    {
        private readonly IIngresoDiarioRepositorio ingresoDiarioRepositorio;

        public IngresoDiarioController(IIngresoDiarioRepositorio _ingresoDiarioRepositorio)
        {
            ingresoDiarioRepositorio = _ingresoDiarioRepositorio;
        }

        [HttpPost("sinMarca")]
        public async Task<IActionResult> IngresosPendientes([FromBody] VMFecha fechaActual)
        {
                List<IngresoDiarioDTO> ingresos = await ingresoDiarioRepositorio.ObtenerSinMarcaciones(fechaActual.FechaActual);
                if (ingresos == null)
                {
                    return BadRequest();
                }
                return Ok(ingresos);
        }

        [HttpPost("Actualizar")]
        public async Task<IActionResult> ActualizarIngreso([FromBody] IngresoDiarioDTO ingresoDiarioDTO)
        {
            if (ingresoDiarioDTO != null)
            {
                var resultado = await ingresoDiarioRepositorio.Actualizar(ingresoDiarioDTO);
                if (resultado == null)
                {
                    return BadRequest();
                }
                return Ok(resultado);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
