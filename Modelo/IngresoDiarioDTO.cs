using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class IngresoDiarioDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public DateTime EntradaPlanificada { get; set; }
        [Required]
        public DateTime SalidaPlanificada { get; set; }
        public DateTime EntradaEfectiva { get; set; }
        public DateTime SalidaEfectiva { get; set; }
        public string Comentarios { get; set; }
        public virtual ProveedorDTO Proveedor { get; set; }
        public FuncionarioDTO Funcionario { get; set; }
    }
}
