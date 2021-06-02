using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Data
{
    [Index(nameof(Fecha), nameof(FuncionarioId),IsUnique =true)]
    public class Ingreso
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
        [MaxLength(30)]
        [Required]
        public string EstadoAutorizacion { get; set; }
        [MaxLength(150)]
        public string Comentarios { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}
