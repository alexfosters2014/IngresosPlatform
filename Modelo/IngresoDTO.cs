using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class IngresoDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public FuncionarioDTO Funcionario { get; set; }
        [Required]
        public DateTime EntradaPlanificada { get; set; }
        [Required]
        public DateTime SalidaPlanificada { get; set; }
        [MaxLength(30)]
        [Required]
        public string EstadoAutorizacion { get; set; }
        [MaxLength(150)]
        public string Comentarios { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        [Required]
        public ProveedorDTO Proveedor { get; set; }
    }
}
