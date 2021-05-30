﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatformWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArchivoController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ArchivoController(IWebHostEnvironment _environment, IHttpContextAccessor _httpContextAccessor)
        {
            environment = _environment;
            httpContextAccessor = _httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> SubirPDF([FromForm] IFormFile filePDF)
         {
            if (filePDF == null || filePDF.Length == 0)
            {
                return BadRequest("Subir un archivo PDF");
            }
            string nombreCarpeta = "archivos";
            string folder = Path.Combine(environment.WebRootPath, nombreCarpeta);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(nombreCarpeta);
            }
            string fileName = filePDF.FileName;
            string extension = Path.GetExtension(fileName);
            string[] soloExtensiones = { ".pdf" };

            if (!soloExtensiones.Contains(extension))
            {
                return BadRequest("Extension inválida del archivo");
            }                                           

            string newFile = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(folder, newFile);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await filePDF.CopyToAsync(fileStream);
            }
            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var rutaParaBD = $"{urlActual}/archivos/{newFile}";

            return Ok(rutaParaBD);
        }

    }
    }