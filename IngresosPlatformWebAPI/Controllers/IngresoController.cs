using Comun;
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
    public class IngresoController : Controller
    {
        private readonly IIngresoRepositorio ingresoRepositorio;

        public IngresoController(IIngresoRepositorio _ingresoRepositorio)
        {
            ingresoRepositorio = _ingresoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> IngresosPendientes()
        {
            List<IngresoDTO> ingresos = await ingresoRepositorio.ObtenerPendientes();
            if (ingresos == null)
            {
                return BadRequest();
            }
            if (ingresos.Count > 0)
            {

                List<IngresoXProveedorDTO> ingXProveedor = ingresos
                                      .GroupBy(g => g.Proveedor)
                                      .Select(s => new IngresoXProveedorDTO()
                                      {
                                          Proveedor = s.Key,
                                          Ingresos = s.ToList()
                                      }).ToList();

                return Ok(ingXProveedor);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> NuevosIngresos([FromBody] List<IngresoDTO> ingresosNuevos)
        {
            // ingreso nuevo
            if (ingresosNuevos != null && ingresosNuevos.Count > 0)
            {
                var resultado = await ingresoRepositorio.Agregar(ingresosNuevos);
                if (resultado == SD.IngresosReturn.ERROR.ToString())
                {
                    return BadRequest(resultado);
                }
                return Ok(resultado);
            }
            else
            {
                return BadRequest(SD.IngresosReturn.ERROR.ToString());
            }
        }

        [HttpPost("Actualizar")]
        public async Task<IActionResult> ActualizarIngreso([FromBody] IngresoDTO ingresoEstado)
        {
            if (ingresoEstado != null)
            {
                var resultado = await ingresoRepositorio.Actualizar(ingresoEstado);
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

        [HttpGet("estado/{ingresoId:int}/{estadoAutorizacion}")]
        public async Task<IActionResult> ActualizarEstadoIngreso(int? ingresoId,string estadoAutorizacion)
        {
            if (ingresoId != null && !string.IsNullOrEmpty(estadoAutorizacion))
            {
                var resultado = await ingresoRepositorio.AutorizarIngreso(ingresoId.Value, estadoAutorizacion);
                if (resultado == false)
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
        [HttpDelete("{ingresoId}")]
        public async Task<IActionResult> BorrarIngreso(int? ingresoId)
        {
            if (ingresoId != null)
            {
                var resultado = await ingresoRepositorio.Borrar(ingresoId.Value);
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

        [HttpGet("ingresosNoAutorizadosxProveedor/{proveedorId:int}")]
        public async Task<IActionResult> IngresosNoAutorizadosXProveedor(int proveedorId)
        {
            List<IngresoDTO> ingresos = await ingresoRepositorio.ObtenerNoAutorizadosxProveedor(proveedorId);
            if (ingresos == null)
            {
                return BadRequest();
            }
                return Ok(ingresos);
        }
        [HttpGet("ingresosAutorizadosxProveedor/{proveedorId:int}")]
        public async Task<IActionResult> IngresosAutorizadosXProveedor(int proveedorId)
        {
            List<IngresoDTO> ingresos = await ingresoRepositorio.ObtenerAutorizadosxProveedor(proveedorId);
            if (ingresos == null)
            {
                return BadRequest();
            }
            return Ok(ingresos);
        }



    }
}
