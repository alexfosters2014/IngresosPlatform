using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ConfigMail
    {
        public string DominioSmtp { get; set; }
        public int Puerto { get; set; }
        public bool Ssl { get; set; }
        public string Correo { get; set; }
        public string Pass { get; set; }

    }
}
