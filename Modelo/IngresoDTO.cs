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
        public DateTime? Fecha { get; set; }
        [Required(ErrorMessage = "Debe ingresar un funcionario")]
        public FuncionarioDTO Funcionario { get; set; }
        [Required(ErrorMessage = "Debe ingresar un horario de entrada")]
        [DataType(DataType.Time,ErrorMessage ="Debe ingresar un horario de entrada")]
        public DateTime? EntradaPlanificada { get; set; }
        [Required(ErrorMessage = "Debe ingresar un horario de salida")]
        [DataType(DataType.Time, ErrorMessage = "Debe ingresar un horario de salida")]
        public DateTime? SalidaPlanificada { get; set; }
        [MaxLength(30)]
        public string EstadoAutorizacion { get; set; }
        [MaxLength(150)]
        public string Comentarios { get; set; }
        [Required(ErrorMessage ="Debe ingresar una fecha de inicio")]
        public DateTime? FechaInicio { get; set; }
        [Required(ErrorMessage = "Debe ingresar una fecha de fin")]
        public DateTime? FechaFin { get; set; }
        public ProveedorDTO Proveedor { get; set; }
        public string Indicador { get; set; }
    }
}
