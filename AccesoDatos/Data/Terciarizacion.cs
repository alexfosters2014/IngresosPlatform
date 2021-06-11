using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Data
{
    [Index(nameof(Fecha), nameof(ProveedorId), IsUnique = true)]
    public class Terciarizacion
    {
        [Required]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string PathNomina { get; set; }
        public string PathMtssUnificada { get; set; }
        public string PathCertDgi { get; set; }
        public string PathCertBse { get; set; }
        public string PathCertBps { get; set; }
        public string PathPolizaSeguro { get; set; }
        public string PathPolizaTerciarizacion { get; set; }
        public string PathRespSocial { get; set; }
        public string PathPagoBps { get; set; }
        public string PathPagoBse { get; set; }
        public string PathPagoDgi { get; set; }
        public string PathRecibosSueldo { get; set; }
        public string PathRafagas { get; set; }
        public string PathOtrosDocumentos { get; set; }
        public string Comentarios { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}
