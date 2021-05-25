using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
      
        public string UsuarioNombre { get; set; }
    
        public string Token { get; set; }
     
        public string TipoUsuario { get; set; }
        public string Email { get; set; }

        public string PassInicial { get; set; }

        public ProveedorDTO Proveedor { get; set; }
    }
}
