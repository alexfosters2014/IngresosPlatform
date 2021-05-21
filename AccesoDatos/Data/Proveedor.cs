using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Data
{
    public class Proveedor
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(12)]
        public string Rut { get; set; }
        [Required]
        [MaxLength(100)]
        public string RazonSocial { get; set; }
        [MaxLength(100)]
        public string NombreFantasia { get; set; }
        [MaxLength(60)]
        public string Direccion { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
