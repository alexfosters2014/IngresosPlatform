using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class IngresoDiarioxProveedor
    {
        public int ProveedorId{ get; set; }
        public List<IngresoDiarioDTO> IngresosDiarios { get; set; }
    }
}
