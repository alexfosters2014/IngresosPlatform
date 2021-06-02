using Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Data
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string UsuarioNombre { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        [MaxLength(300)]
        public string Token { get; set; }
        [Required]
        public string TipoUsuario { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}
