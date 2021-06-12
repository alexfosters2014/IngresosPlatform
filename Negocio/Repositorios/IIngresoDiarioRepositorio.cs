using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Repositorios
{
    public interface IIngresoDiarioRepositorio
    {
        public Task<int> Agregar(IngresoDTO ingresoDTO);
        public Task<int> Borrar(int ingresoId);
        public Task<IngresoDiarioDTO> Actualizar(IngresoDiarioDTO funcionarioDTO);
        public Task<List<IngresoDiarioDTO>> ObtenerTodosHorariosEfectivos(VMGeneral vMGeneral);
        public Task<List<IngresoDiarioDTO>> ObtenerTodosHorariosPlanificados(VMGeneral vMGeneral);
        public Task<List<IngresoDiarioDTO>> ObtenerSinMarcaciones(DateTime fecha);
        public Task<int> CantidadIngresosDia(DateTime fecha);
    }
}
