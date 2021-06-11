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
    public class TerciarizacionController : Controller
    {
        private readonly ITerciarizacionRepositorio terciarizacionRepositorio;

        public TerciarizacionController(ITerciarizacionRepositorio _terciarizacionRepositorio)
        {
            terciarizacionRepositorio = _terciarizacionRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> NuevoActualizar([FromBody] TerciarizacionDTO terciarizacionDTO)
        {
            if (terciarizacionDTO != null && terciarizacionDTO.Id == 0)
            {
                //nuevo funcionario
                var resultado = await terciarizacionRepositorio.Agregar(terciarizacionDTO);
                if (resultado == null)
                {
                    return BadRequest();
                }
                return Ok(resultado);
            }
            else
            {
                var resultado = await terciarizacionRepositorio.Actualizar(terciarizacionDTO);
                if (resultado == null)
                {
                    return BadRequest();
                }

                return Ok(resultado);
            }
        }
        [HttpGet("{tercId}")]
        public async Task<IActionResult> ObtenerIndividual(int tercId)
        {
            if (tercId > 0)
            {
                //nuevo funcionario
                var resultado = await terciarizacionRepositorio.Obtener(tercId);
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

        [HttpGet("/ObtenerTodos/{proveedorId}")]
        public async Task<IActionResult> ObtenerTodosXProveedor(int proveedorId)
        {
            if (proveedorId > 0)
            {
                //nuevo funcionario
                var resultado = await terciarizacionRepositorio.ObtenerTodosXProveedor(proveedorId);
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

        [HttpPost("/ObtenerTodos")]
        public async Task<IActionResult> ObtenerTodos(VMFecha vmFecha)
        {
            if (vmFecha != null)
            {
                //nuevo funcionario
                var resultado = await terciarizacionRepositorio.ObtenerTodos(vmFecha);
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
