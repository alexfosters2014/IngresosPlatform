using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class VMCambioPass
    {
        [StringLength(16, ErrorMessage = "Debe tener entre 6 y 16 caracteres", MinimumLength = 6)]
        [Required(ErrorMessage = "Es necesario una contraseña")]
        public string Password { get; set; }
        [StringLength(16, ErrorMessage = "Debe tener entre 6 y 16 caracteres", MinimumLength = 6)]
        [Required(ErrorMessage = "Es necesaria la confirmación de la contraseña")]
        [Compare("Password",ErrorMessage ="Las contraseñas no son iguales. Verifique")]
        public string ConfirmacionPassword { get; set; }
    }
}
