using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaModelos;

namespace ServiciosAPI.Interfaces
{
    public interface IConsecutivoService
    {
        Task<List<Consecutivo>> GetConsecutivosAsync();
        Task<List<Consecutivo>> GetAllConsecutivosAsync(); // Incluye inactivos para cálculo de IDs
        Task<List<Consecutivo>> PutConsecutivoAsync(Consecutivo consecutivo);
        Task<Consecutivo> GetConsecutivoByIdAsync(int id);
        Task<Consecutivo> PostConsecutivoAsync(Consecutivo consecutivo);
        Task<List<Consecutivo>> DeleteConsecutivoAsync(int id);
    }
}
