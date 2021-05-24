using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ConfiguracionSistemaDTO
    {
        public int Id { get; set; }
        public int AnticipacionVtos { get; set; }
        //configuracion para el envio de mail
        [Required(ErrorMessage = "El dominio no puede estar vacio")]
        [MaxLength(70)]
        public string DominioSmtp { get; set; }
        [Required(ErrorMessage = "El puerto no puede estar vacio")]
        [MaxLength(10)]
        public int puerto { get; set; }
        public bool Ssl { get; set; }
        [Required(ErrorMessage = "El correo propio no puede estar vacio")]
        [MaxLength(100)]
        public string correo { get; set; }
        [Required(ErrorMessage = "La contraseña no puede estar vacia")]
        [MaxLength(40)]
        public string passMail { get; set; }
    }
}
