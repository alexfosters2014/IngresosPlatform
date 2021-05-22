using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ProveedorDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El RUT debe contener 12 digitos")]
        [MaxLength(12)]
        public string Rut { get; set; }
        [Required(ErrorMessage = "La razón social no puede estar vacia")]
        [MaxLength(100)]
        public string RazonSocial { get; set; }
        [MaxLength(100)]
        public string NombreFantasia { get; set; }
        [MaxLength(60)]
        public string Direccion { get; set; }
        [MaxLength(50)]
        public string Rubro { get; set; }
        [Required(ErrorMessage = "El campo está vacio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El formato de email no es válido")]
        public string Email { get; set; }
    }
}
