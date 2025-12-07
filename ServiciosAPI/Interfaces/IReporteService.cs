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
        Task<List<Reporte>> GetReportesAsync();
        Task<List<Reporte>> PutReporteAsync(Reporte reporte);
        Task<Reporte> GetReporteByIdAsync(int id);
        Task<List<Reporte>> PostReporteAsync(Reporte reporte);
        Task<List<Reporte>> DeleteReporteAsync(int id);
    }
}
