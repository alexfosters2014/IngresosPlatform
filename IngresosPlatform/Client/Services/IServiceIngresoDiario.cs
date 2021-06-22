using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    public interface IServiceIngresoDiario
    {
        public Task<IngresoDiarioDTO> ActualizarMarcacion(IngresoDiarioDTO ingresoDiarioDTO, string token);
        public Task<List<IngresoDiarioxProveedor>> ObtenerSinMarcaciones(VMGeneral fechaActual, string token);
        public Task<List<IngresoDiarioxProveedor>> ObtenerReporteHorariosEfectivos(VMGeneral vmGeneral, string token);
        public Task<List<IngresoDiarioxProveedor>> ObtenerReporteHorariosPlanificados(VMGeneral vmGeneral, string token);
        public Task<VMGeneral> ObtenerFechaAPI(string token);
    }
}
