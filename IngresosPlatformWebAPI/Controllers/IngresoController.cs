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

        [HttpPut]
        public async Task<IActionResult> NuevoIngreso([FromBody] List<IngresoDTO> ingresoNuevo)
        {
            // ingreso nuevo
            if (ingresoNuevo != null)
            {
                var resultado = await ingresoRepositorio.Agregar(ingresoNuevo);
                if (resultado == 0)
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
        [HttpPost]
        public async Task<IActionResult> ActualizarEstadoIngreso([FromBody] IngresoDTO ingresoEstado)
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





        }
}
