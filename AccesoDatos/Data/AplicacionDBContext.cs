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
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //claves primarias compuestas
        //    modelBuilder.Entity<Funcionario>()
        //   .HasKey(t => new { t.Id, t.Nombre });
        //}

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<IngresoDiario> IngresosDiarios { get; set; }
        public DbSet<Terciarizacion> Terciarizaciones { get; set; }
    }
}
