using AccesoDatos.Data;
using AutoMapper;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProveedorDTO, Proveedor>().ReverseMap();
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            CreateMap<ConfigMail, MailDTO>().ReverseMap();
            CreateMap<IngresoDTO, Ingreso>().ReverseMap();
            CreateMap<FuncionarioDTO, Funcionario>().ReverseMap();
            CreateMap<IngresoDiarioDTO, IngresoDiario>().ReverseMap();
            CreateMap<IngresoDiarioDTO, IngresoDTO>().ReverseMap();
            CreateMap<TerciarizacionDTO, Terciarizacion>().ReverseMap();
        }
    }
}
