using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Data
{
    public class AplicacionDBContext : DbContext
    {
        public AplicacionDBContext(DbContextOptions<AplicacionDBContext> options) : base(options)
        {
           
        }
        
        DbSet<Proveedor> Proveedores { get; set; }
        DbSet<Funcionario> Funcionarios { get; set; }
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Ingreso> Ingresos { get; set; }
    }
}
