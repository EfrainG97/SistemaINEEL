using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaModelos;

namespace ServiciosAPI.Interfaces
{
    public interface ISistemaService
    {
        #region Métodos GET
        Task<List<Sistema>> GetSistemasAsync();
        Task<Sistema> GetSistemaByIdAsync(int id);
        #endregion

        #region Métodos POST
        Task<List<Sistema>> PostSistemaAsync(Sistema sistema);
        #endregion

        #region Métodos PUT
        Task<List<Sistema>> PutSistemaAsync(Sistema sistema);
        #endregion

        #region Métodos DELETE
        Task<List<Sistema>> DeleteSistemaAsync(int id);
        #endregion
    }
}
