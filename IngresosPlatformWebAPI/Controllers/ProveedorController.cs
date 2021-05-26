using Comun;
using Microsoft.AspNetCore.Http;
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
    public class ProveedorController : Controller
    {
        private readonly IProveedorRepositorio proveedorRepositorio;
        
        public ProveedorController(IProveedorRepositorio _proveedorRepositorio)
        {
            proveedorRepositorio = _proveedorRepositorio;
        }
        [HttpGet]
        public async Task<IActionResult> Proveedores()
        {
            var todosLosProveedores = await proveedorRepositorio.ObtenerProveedores();
            return Ok(todosLosProveedores);
        }

        [HttpGet("{proveedorId}")]
        public async Task<IActionResult> Proveedores(int? proveedorId)
        {
            if (proveedorId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Titulo = "",
                    ErrorMensaje = "Id de proveedor inválido o nulo",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var proveedor = await proveedorRepositorio.ObtenerProveedor(proveedorId.Value);
            if (proveedor == null)
            {
                return BadRequest(new ErrorModel()  
                {
                    Titulo = "",
                    ErrorMensaje = "Proveedor no existe",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(proveedor);
        }
        [HttpPost]
        public async Task<IActionResult> Proveedor([FromBody] ProveedorDTO proveedorDTO)
        {
            if (proveedorDTO != null && proveedorDTO.Id == 0) { 
            var resultado = await proveedorRepositorio.AgregarProveedor(proveedorDTO);
            if (resultado== null)
            {
                return BadRequest(new ErrorModel()
                {
                    Titulo = "",
                    ErrorMensaje = "Proveedor ya existe o no puedo crearse",
                    StatusCode = StatusCodes.Status400BadRequest
                });;
            }
            return Ok(resultado);
            }
            else
            {
                var resultado = await proveedorRepositorio.ActualizarProveedor(proveedorDTO.Id, proveedorDTO);
                if (resultado == null)
                {
                    return BadRequest(new ErrorModel()
                    {
                        Titulo = "",
                        ErrorMensaje = "Proveedor ya existe o no puedo crearse",
                        StatusCode = StatusCodes.Status400BadRequest
                    }); ;
                }

                return Ok(resultado);
            }
        }

        [HttpDelete("{proveedorId}")]
        public async Task<IActionResult> Delete(int? proveedorId)
        {
            if (proveedorId != null) {
            int resultado = await proveedorRepositorio.EliminarProveedor(proveedorId.Value);
            if (resultado == 0)
            {
                return BadRequest(null); ;
            }
            return Ok(resultado);
        }else
            {
                return BadRequest(null);
            }

        }

    }
}
