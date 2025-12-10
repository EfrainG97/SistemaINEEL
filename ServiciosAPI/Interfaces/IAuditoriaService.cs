using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaModelos;

namespace ServiciosAPI.Interfaces
{
    public interface IAuditoriaService
    {
        #region Métodos GET
        Task<List<Auditoria>> GetAuditoriasAsync();
        Task<Auditoria> GetAuditoriaByIdAsync(int id);
        #endregion

        #region Métodos POST
        Task<List<Auditoria>> PostAuditoriaAsync(Auditoria auditoria);
        #endregion

        #region Métodos PUT
        Task<List<Auditoria>> PutAuditoriaAsync(Auditoria auditoria);
        #endregion

        #region Métodos DELETE
        Task<List<Auditoria>> DeleteAuditoriaAsync(int id);
        #endregion
    }
}
