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
        public async Task<IActionResult> IngresosDiariosSinMarcaciones([FromBody] VMGeneral fechaActual)
        {
            if (fechaActual.FechaActual <= DateTime.Today.Date)
            {
                List<IngresoDiarioDTO> ingresos = await ingresoDiarioRepositorio.ObtenerSinMarcaciones(fechaActual.FechaActual);
                if (ingresos == null)
                {
                    return BadRequest();
                }
                List<IngresoDiarioxProveedor> ingXProveedor = ingresos
                                     .GroupBy(g => g.Proveedor.Id)
                                     .Select(s => new IngresoDiarioxProveedor()
                                     {
                                         ProveedorId = s.Key,
                                         IngresosDiarios = s.ToList()
                                     }).ToList();

                return Ok(ingXProveedor);
            }
            else
            {
                List<IngresoDiarioDTO> ingresos = new List<IngresoDiarioDTO>();
                return Ok(ingresos);
            }
        }

        [HttpPost("Actualizar")]
        public async Task<IActionResult> ActualizarIngresoDiario([FromBody] IngresoDiarioDTO ingresoDiarioDTO)
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
