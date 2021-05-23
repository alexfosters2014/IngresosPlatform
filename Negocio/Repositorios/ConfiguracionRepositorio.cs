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

        public async Task<ConfiguracionSistemaDTO> ActualizarConfig(ConfiguracionSistemaDTO ConfiguracionSistemaDTO)
        {
            try
            {
                int configId = 1;
                ConfiguracionSistema configDB = await db.Configuraciones.FindAsync(configId);
                ConfiguracionSistema config = mapper.Map<ConfiguracionSistemaDTO, ConfiguracionSistema>(ConfiguracionSistemaDTO, configDB);
                var updateConfig = db.Configuraciones.Update(config);
                    await db.SaveChangesAsync();
                    return mapper.Map<ConfiguracionSistema, ConfiguracionSistemaDTO>(updateConfig.Entity);
              
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ConfiguracionSistemaDTO> AgregarConfig(ConfiguracionSistemaDTO ConfiguracionSistemaDTO)
        {
            try
            {
                ConfiguracionSistema config = mapper.Map<ConfiguracionSistemaDTO, ConfiguracionSistema>(ConfiguracionSistemaDTO);
                var addConfig = await db.Configuraciones.AddAsync(config);
                await db.SaveChangesAsync();
                return mapper.Map<ConfiguracionSistema, ConfiguracionSistemaDTO>(addConfig.Entity);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ConfiguracionSistemaDTO> ObtenerConfig()
        {
            try
            {
                int configId = 1;
                ConfiguracionSistemaDTO config = mapper.Map<ConfiguracionSistema, ConfiguracionSistemaDTO>(await db.Configuraciones.FindAsync(configId));
                 return config;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
