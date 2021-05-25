using AccesoDatos.Data;
using AutoMapper;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public class MailRepositorio : IMailRepositorio
    {
        private readonly AplicacionDBContext db;
        private readonly IMapper mapper;

        public MailRepositorio(AplicacionDBContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        public async Task<MailDTO> CargarConfigMail()
        {
            try
            {
                ConfiguracionSistemaDTO configSistema = mapper.Map<ConfiguracionSistema, ConfiguracionSistemaDTO>(await db.Configuraciones.FindAsync(1));
                MailDTO mailDTO = mapper.Map<ConfiguracionSistemaDTO, MailDTO>(configSistema);
                return mailDTO;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
