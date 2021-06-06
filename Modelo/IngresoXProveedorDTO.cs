using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class IngresoXProveedorDTO
    {
        public int ProveedorId { get; set; }
        public List<IngresoDTO> Ingresos { get; set; }
    }
}
