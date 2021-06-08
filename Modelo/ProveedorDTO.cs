using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ProveedorDTO : IEquatable<ProveedorDTO>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El RUT no puede estar vacio")]
        [MaxLength(12, ErrorMessage = "El RUT debe contener 12 digitos")]
        public string Rut { get; set; }
        [Required(ErrorMessage = "La razón social no puede estar vacia")]
        [MaxLength(100)]
        public string RazonSocial { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "El nombre fantasia no puede estar vacio")]
        public string NombreFantasia { get; set; }
        [MaxLength(60)]
        public string Direccion { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "El rubro no puede estar vacio")]
        public string Rubro { get; set; }
        [Required(ErrorMessage = "El email no puede estar vacio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El formato de email no es válido")]
        public string Email { get; set; }
        public bool Activo { get; set; }

        public bool Equals(ProveedorDTO other)
        {
            if (other != null)
            {
                if (other is ProveedorDTO)
                {
                    if (this.Id == other.Id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
