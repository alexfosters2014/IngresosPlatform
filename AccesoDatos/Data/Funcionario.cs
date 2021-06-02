using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Data
{
    [Index(nameof(Cedula), IsUnique = true)]
    public class Funcionario
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(9)]
        public string Cedula { get; set; }
        [Required]
        public DateTime VtoCedula { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        public string PathCedula { get; set; }
        [MaxLength(10)]
        public string CategoriaLibreta { get; set; }
        public DateTime? VtoLibreta { get; set; }
        public string PathLibreta { get; set; }
        [Required]
        public DateTime? VtoCarneSalud { get; set; }
        public string PathCarneSalud { get; set; }
        public DateTime? VtoCMAlimentos { get; set; }
        public string PathCMAlimentos { get; set; }
        [Required]
        public DateTime? AltaBps { get; set; }
        public string PathAltaBps { get; set; }
        [Required]
        public virtual Proveedor Proveedor { get; set; }
        public bool Activo { get; set; }
    }
}
