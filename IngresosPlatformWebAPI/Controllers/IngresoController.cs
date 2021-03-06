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
        private readonly IIngresoDiarioRepositorio ingresoDiarioRepositorio;

        public IngresoController(IIngresoRepositorio _ingresoRepositorio, IIngresoDiarioRepositorio _ingresoDiarioRepositorio)
        {
            ingresoRepositorio = _ingresoRepositorio;
            ingresoDiarioRepositorio = _ingresoDiarioRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> IngresosPendientes()
        {
            List<IngresoDTO> ingresos = await ingresoRepositorio.ObtenerPendientes();
            if (ingresos == null)
            {
                return BadRequest();
            }
                List<IngresoXProveedorDTO> ingXProveedor = ingresos
                                      .GroupBy(g => g.Proveedor.Id)
                                      .Select(s => new IngresoXProveedorDTO()
                                      {
                                          ProveedorId = s.Key,
                                          Ingresos = s.Select(n => new IngresoDTO() { 
                                                    Id = n.Id,
                                                    Fecha = n.Fecha,
                                                    Funcionario = n.Funcionario,
                                                    EntradaPlanificada = n.EntradaPlanificada,
                                                    SalidaPlanificada = n.SalidaPlanificada,
                                                    EstadoAutorizacion = n.EstadoAutorizacion,
                                                    Comentarios = n.Comentarios,
                                                    FechaInicio = n.FechaInicio,
                                                    FechaFin = n.FechaFin,
                                                    Proveedor = n.Proveedor,
                                                    Indicador = ((n.Funcionario.VtoCedula <= n.FechaFin.Value || 
                                                                  n.Funcionario.VtoLibreta <= n.FechaFin.Value ||
                                                                  n.Funcionario.VtoCarneSalud <= n.FechaFin.Value ||
                                                                  n.Funcionario.VtoCMAlimentos <= n.FechaFin.Value) ? $"{SD.IndicadorVto.ROJO.ToString()}.png" :

                                                                  (n.Funcionario.VtoCedula <= n.FechaFin.Value.AddDays(15) ||
                                                                  n.Funcionario.VtoLibreta <= n.FechaFin.Value.AddDays(15) ||
                                                                  n.Funcionario.VtoCarneSalud <= n.FechaFin.Value.AddDays(15) ||
                                                                  n.Funcionario.VtoCMAlimentos <= n.FechaFin.Value.AddDays(15)) ? $"{SD.IndicadorVto.NARANJA.ToString()}.png" :

                                                                   (n.Funcionario.VtoCedula <= n.FechaFin.Value.AddDays(30) ||
                                                                  n.Funcionario.VtoLibreta <= n.FechaFin.Value.AddDays(30) ||
                                                                  n.Funcionario.VtoCarneSalud <= n.FechaFin.Value.AddDays(30) ||
                                                                  n.Funcionario.VtoCMAlimentos <= n.FechaFin.Value.AddDays(30)) ? $"{SD.IndicadorVto.AMARILLO.ToString()}.png" : 
                                                                  $"{SD.IndicadorVto.OK.ToString()}.png"  )
                                          }).ToList()
                                      }).ToList();

                return Ok(ingXProveedor);
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
                // registrar el ingreso
                var resultado2 = await ingresoDiarioRepositorio.Agregar(resultado);
                if (resultado2 < 0)
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

        [HttpGet("ingresosAutYPendxProveedor/{proveedorId:int}")]
        public async Task<IActionResult> IngresosAutorizadosYPendientesXProveedor(int proveedorId)
        {
            List<IngresoDTO> ingresos = await ingresoRepositorio.ObtenerAutorizadosYPendientesxProveedor(proveedorId);
            if (ingresos == null)
            {
                return BadRequest();
            }
            return Ok(ingresos);
        }


    }
}
