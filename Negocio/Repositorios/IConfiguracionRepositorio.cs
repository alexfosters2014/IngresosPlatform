using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public interface IConfiguracionRepositorio
    {
        public Task<ConfigMail> AgregarConfig(ConfigMail ConfiguracionSistemaDTO);
        public Task<ConfigMail> ActualizarConfig(ConfigMail ConfiguracionSistemaDTO);
        public Task<ConfigMail> ObtenerConfig();
    }
}
