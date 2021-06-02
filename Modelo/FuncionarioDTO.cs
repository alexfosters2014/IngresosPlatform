using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class FuncionarioDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Debe ingresar un numero de cedula")]
        [MaxLength(12,ErrorMessage = "Maximo 12 caracteres")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "Debe ingresar Vto Cedula")]
        public DateTime VtoCedula { get; set; } = DateTime.Now;
        [Required(ErrorMessage ="Debe ingresar nombre y apellido")]
        [MaxLength(100)]
        public string Nombre { get; set; }
        public string PathCedula { get; set; }
        [MaxLength(10,ErrorMessage = "Debe ingresar una categoria hasta 10 caracteres")]
        public string CategoriaLibreta { get; set; }
        public DateTime? VtoLibreta { get; set; }
        public string PathLibreta { get; set; }
        [Required(ErrorMessage = "Debe ingresar vto carne de salud")]
        public DateTime? VtoCarneSalud { get; set; }
        [Required(ErrorMessage = "Debe subir un archivo en formato PDF")]
        public string PathCarneSalud { get; set; }
        [Required(ErrorMessage = "Debe ingresar un vto de carne de manipulacion de alimentos")]
        public DateTime? VtoCMAlimentos { get; set; }
        [Required(ErrorMessage = "Debe subir un archivo en formato PDF")]
        public string PathCMAlimentos { get; set; }
        [Required(ErrorMessage = "Debe ingresar fecha de alta BPS")]
        public DateTime? AltaBps { get; set; }
        [Required(ErrorMessage = "Debe subir un archivo en formato PDF")]
        public string PathAltaBps { get; set; }
        public ProveedorDTO Proveedor { get; set; }
        public bool Activo { get; set; } = true;
    }
}
