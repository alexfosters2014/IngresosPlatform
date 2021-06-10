using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public interface IMailRepositorio
    {
        public Task<MailDTO> CargarConfigMail(ConfigMail configMail);
    }
}
