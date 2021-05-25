﻿// <auto-generated />
using System;
using AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AccesoDatos.Migrations
{
    [DbContext(typeof(AplicacionDBContext))]
    partial class AplicacionDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AccesoDatos.Data.ConfiguracionSistema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnticipacionVtos")
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DominioSmtp")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("PassMail")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Puerto")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<bool>("Ssl")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Configuraciones");
                });

            modelBuilder.Entity("AccesoDatos.Data.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AltaBps")
                        .HasColumnType("datetime2");

                    b.Property<string>("CategoriaLibreta")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PathAltaBps")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PathCMAlimentos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PathCarneSalud")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PathCedula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PathLibreta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("VtoCMAlimentos")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VtoCarneSalud")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VtoCedula")
                        .HasColumnType("datetime2");

                    b.Property<string>("VtoLibreta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Cedula")
                        .IsUnique();

                    b.HasIndex("ProveedorId");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("AccesoDatos.Data.Ingreso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comentarios")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("EntradaEfectiva")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EntradaPlanificada")
                        .HasColumnType("datetime2");

                    b.Property<string>("EstadoAutorizacion")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FuncionarioId")
                        .HasColumnType("int");

                    b.Property<int?>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SalidaEfectiva")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SalidaPlanificada")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("Ingresos");
                });

            modelBuilder.Entity("AccesoDatos.Data.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Direccion")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreFantasia")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Rubro")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Rut")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("Id");

                    b.HasIndex("Rut")
                        .IsUnique();

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("AccesoDatos.Data.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<string>("TipoUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("UsuarioNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("AccesoDatos.Data.Funcionario", b =>
                {
                    b.HasOne("AccesoDatos.Data.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("AccesoDatos.Data.Ingreso", b =>
                {
                    b.HasOne("AccesoDatos.Data.Funcionario", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionarioId");

                    b.HasOne("AccesoDatos.Data.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId");

                    b.Navigation("Funcionario");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("AccesoDatos.Data.Usuario", b =>
                {
                    b.HasOne("AccesoDatos.Data.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId");

                    b.Navigation("Proveedor");
                });
#pragma warning restore 612, 618
        }
    }
}
