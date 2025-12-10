using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaModelos;

namespace ServiciosAPI.Interfaces
{
    public interface IReporteService
    {
        #region Métodos GET
        Task<List<Reporte>> GetReportesAsync();
        Task<Reporte> GetReporteByIdAsync(int id);
        #endregion

        #region Métodos POST
        Task<List<Reporte>> PostReporteAsync(Reporte reporte);
        #endregion

        #region Métodos PUT
        Task<List<Reporte>> PutReporteAsync(Reporte reporte);
        #endregion

        #region Métodos DELETE
        Task<List<Reporte>> DeleteReporteAsync(int id);
        #endregion
    }
}
