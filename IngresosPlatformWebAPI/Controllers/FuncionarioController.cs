using Comun;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
                var funcionariosActPRov = await funcionarioRepositorio.ObtenerTodosSegunProveedor(proveedorId.Value);
                if (funcionariosActPRov != null) 
                {
                    DateTime hoy = DateTime.Today.Date;
                   foreach(FuncionarioDTO fun in funcionariosActPRov)
                    {
                        DateTime? vtoLibreta = fun.VtoLibreta != null ? fun.VtoLibreta.Value : DateTime.MaxValue.Date;
                        DateTime? vtoCarneSalud = fun.VtoCarneSalud != null ? fun.VtoCarneSalud.Value : DateTime.MaxValue.Date;
                        DateTime? vtoCMAlimentos = fun.VtoCMAlimentos != null ? fun.VtoCMAlimentos.Value : DateTime.MaxValue.Date;

                        if (hoy >= fun.VtoCedula ||
                           hoy >= vtoLibreta.Value ||
                           hoy >= vtoCarneSalud ||
                           hoy >= vtoCMAlimentos)
                        {
                            fun.Indicador = $"{SD.IndicadorVto.ROJO.ToString()}.png";
                        }
                        else if (hoy >= fun.VtoCedula.AddDays(-15) ||
                                  hoy >= vtoLibreta.Value.AddDays(-15) ||
                                  hoy >= vtoCarneSalud.Value.AddDays(-15) ||
                                  hoy >= vtoCMAlimentos.Value.AddDays(-15) )
                        {
                            fun.Indicador = $"{SD.IndicadorVto.NARANJA.ToString()}.png";
                        }
                        else if (hoy >= fun.VtoCedula.AddDays(-30) ||
                                  hoy >= vtoLibreta.Value.AddDays(-30) ||
                                  hoy >= vtoCarneSalud.Value.AddDays(-30) ||
                                  hoy >= vtoCMAlimentos.Value.AddDays(-30))
                        {
                            fun.Indicador = $"{SD.IndicadorVto.AMARILLO.ToString()}.png";
                        }
                        else
                        {
                            fun.Indicador = $"{SD.IndicadorVto.OK.ToString()}.png";
                        }
                    }

                    return Ok(funcionariosActPRov);
                }
                else
                {
                    return BadRequest();
                }
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
