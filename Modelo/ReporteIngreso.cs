using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ReporteIngreso
    {
        public string TipoFecha { get; set; }
        public string nombreArchivo {get; set; }
        public List<IngresoDiarioxProveedor> IngresosXProveedores { get; set; }
    }
}
