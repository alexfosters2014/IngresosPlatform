using Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Data
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string UsuarioRut { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string TipoUsuario { get; set; }
        [Required]
        public Proveedor Proveedor { get; set; }
    }
}
