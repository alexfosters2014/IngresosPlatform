using AccesoDatos.Data;
using AutoMapper;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Negocio.Repositorios
{
    public class ConfiguracionRepositorio : IConfiguracionRepositorio
    {
        private readonly AplicacionDBContext db;
        private readonly IMapper mapper;

        public ConfiguracionRepositorio(AplicacionDBContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public async Task<ConfigMail> ActualizarConfig(ConfigMail ConfiguracionSistemaDTO)
        {
            try
            {
                int configId = 1;
                ConfiguracionSistema configDB = await db.Configuraciones.FindAsync(configId);
                ConfiguracionSistema config = mapper.Map<ConfigMail, ConfiguracionSistema>(ConfiguracionSistemaDTO, configDB);
                var updateConfig = db.Configuraciones.Update(config);
                    await db.SaveChangesAsync();
                    return mapper.Map<ConfiguracionSistema, ConfigMail>(updateConfig.Entity);
              
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ConfigMail> AgregarConfig(ConfigMail ConfiguracionSistemaDTO)
        {
            try
            {
                ConfiguracionSistema config = mapper.Map<ConfigMail, ConfiguracionSistema>(ConfiguracionSistemaDTO);
                var addConfig = await db.Configuraciones.AddAsync(config);
                await db.SaveChangesAsync();
                return mapper.Map<ConfiguracionSistema, ConfigMail>(addConfig.Entity);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ConfigMail> ObtenerConfig()
        {
            try
            {
                int configId = 1;
                ConfigMail config = mapper.Map<ConfiguracionSistema, ConfigMail>(await db.Configuraciones.FindAsync(configId));
                 return config;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
