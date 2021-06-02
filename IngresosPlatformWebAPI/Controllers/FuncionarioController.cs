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
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioRepositorio funcionarioRepositorio;

        public FuncionarioController(IFuncionarioRepositorio _funcionarioRepositorio)
        {
            funcionarioRepositorio = _funcionarioRepositorio;
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerFuncionarios()
        {
            var funcionariosActivos = await funcionarioRepositorio.ObtenerTodosActivos();
            return Ok(funcionariosActivos);
        }
        [HttpGet("FunProv/{proveedorId}")]
        public async Task<IActionResult> ObtenerFuncionariosXProveedor(int? proveedorId)
        {
            if (proveedorId != null)
            { 
                var funcionariosActivos = await funcionarioRepositorio.ObtenerTodosSegunProveedor(proveedorId.Value);
            return Ok(funcionariosActivos);
             }
            else
            {
                return BadRequest();
             }

        }

        [HttpGet("{funcionarioId}")]
        public async Task<IActionResult> Funcionarios(int? funcionarioId)
        {
            if (funcionarioId == null)
            {
                return BadRequest();
            }
            var funcionario = await funcionarioRepositorio.ObtenerFuncionario(funcionarioId.Value);
            if (funcionario == null)
            {
                return BadRequest();
            }
            return Ok(funcionario);
        }
        [HttpPost]
        public async Task<IActionResult> NuevoActualizarFuncionario([FromBody] FuncionarioDTO funcionarioDTO)
        {
            if (funcionarioDTO != null && funcionarioDTO.Id == 0)
            {
                //nuevo funcionario
                var resultado = await funcionarioRepositorio.Agregar(funcionarioDTO);
                if (resultado == null)
                {
                    return BadRequest();
                }
                return Ok(resultado);
            }
            else
            {
                var resultado = await funcionarioRepositorio.Actualizar(funcionarioDTO);
                if (resultado == null)
                {
                    return BadRequest();
                }

                return Ok(resultado);
            }
        }

        [HttpDelete("{funcionarioId}")]
        public async Task<IActionResult> Delete(int? funcionarioId)
        {
            if (funcionarioId != null)
            {
                int resultado = await funcionarioRepositorio.Borrar(funcionarioId.Value);
                if (resultado == 0)
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
