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
        #region Métodos GET
        Task<List<Consecutivo>> GetConsecutivosAsync();
        Task<List<Consecutivo>> GetAllConsecutivosAsync();
        Task<Consecutivo> GetConsecutivoByIdAsync(int id);
        #endregion

        #region Métodos POST
        Task<Consecutivo> PostConsecutivoAsync(Consecutivo consecutivo);
        #endregion

        #region Métodos PUT
        Task<List<Consecutivo>> PutConsecutivoAsync(Consecutivo consecutivo);
        #endregion

        #region Métodos DELETE
        Task<List<Consecutivo>> DeleteConsecutivoAsync(int id);
        #endregion
    }
}
