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
        public async Task<MailDTO> CargarConfigMail(ConfigMail configMail)
        {
            try
            {
                MailDTO mailDTO = mapper.Map<ConfigMail, MailDTO>(configMail);
                return mailDTO;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
