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

        [HttpGet("FechaActual")]
        public async Task<IActionResult> FechaActual()
        {
            return Ok(new VMGeneral(){ FechaActual = DateTime.Today.Date , FechaFin = DateTime.Today.Date,ProveedorId=0});
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

        [HttpPost("ReporteHorariosEfectivos")]
        public async Task<IActionResult> ObtenerTodosHorariosEfectivos([FromBody] VMGeneral vMGeneral)
        {
            if (vMGeneral != null && vMGeneral.FechaActual <= DateTime.Today.Date && vMGeneral.FechaFin >= DateTime.Today.Date)
            {
                List<IngresoDiarioDTO> ingresos = await ingresoDiarioRepositorio.ObtenerTodosHorariosEfectivos(vMGeneral);
                if (ingresos == null)
                {
                    return BadRequest();
                }
                List<IngresoDiarioxProveedor> ingXProveedor = ingresos
                                     .GroupBy(g => g.Proveedor.Id)
                                     .Select(s => new IngresoDiarioxProveedor()
                                     {
                                         ProveedorId = s.Key,
                                         IngresosDiarios = s.OrderBy(o => o.Fecha).ToList()
                                     }).ToList();

                return Ok(ingXProveedor);
            }
            else
            {
                List<IngresoDiarioDTO> ingresos = new List<IngresoDiarioDTO>();
                return Ok(ingresos);
            }
        }
        [HttpPost("ReporteHorariosPlanificados")]
        public async Task<IActionResult> ObtenerTodosHorariosPlanificados([FromBody] VMGeneral vMGeneral)
        {
            if (vMGeneral != null && vMGeneral.FechaActual <= DateTime.Today.Date && vMGeneral.FechaFin >= DateTime.Today.Date)
            {
                vMGeneral.FechaActual = DateTime.Today.Date;
                List<IngresoDiarioDTO> ingresos = await ingresoDiarioRepositorio.ObtenerTodosHorariosPlanificados(vMGeneral);
                if (ingresos == null)
                {
                    return BadRequest();
                }
                List<IngresoDiarioxProveedor> ingXProveedor = ingresos
                                     .GroupBy(g => g.Proveedor.Id)
                                     .Select(s => new IngresoDiarioxProveedor()
                                     {
                                         ProveedorId = s.Key,
                                         IngresosDiarios = s.OrderBy(o => o.Fecha).ToList()
                                     }).ToList();

                return Ok(ingXProveedor);
            }
            else
            {
                List<IngresoDiarioDTO> ingresos = new List<IngresoDiarioDTO>();
                return Ok(ingresos);
            }
        }

    }
}