using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class VMLogin
    {
        [Required(ErrorMessage ="El usuario no puede estar vacio")]
        public string Usuario { get; set; }
        [Required(ErrorMessage ="Es necesaria una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
