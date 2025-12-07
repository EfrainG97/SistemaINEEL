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
        Task<List<Auditoria>> GetAuditoriasAsync();
        Task<List<Auditoria>> PutAuditoriaAsync(Auditoria auditoria);
        Task<Auditoria> GetAuditoriaByIdAsync(int id);
        Task<List<Auditoria>> PostAuditoriaAsync(Auditoria auditoria);
        Task<List<Auditoria>> DeleteAuditoriaAsync(int id);
    }
}
