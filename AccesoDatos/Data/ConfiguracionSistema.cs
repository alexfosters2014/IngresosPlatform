using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Data
{
    public class ConfiguracionSistema
    {
        public int Id { get; set; }
        public int AnticipacionVtos { get; set; }
        //configuracion para el envio de mail
        [MaxLength(70)]
        public string DominioSmtp { get; set; }
        [MaxLength(10)]
        public int Puerto { get; set; }
        public bool Ssl { get; set; }
        [MaxLength(100)]
        public string Correo { get; set; }
        [MaxLength(200)]
        public string PassMail { get; set; }

    }
}
