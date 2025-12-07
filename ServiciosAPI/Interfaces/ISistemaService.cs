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
        Task<List<Sistema>> GetSistemasAsync();
        Task<List<Sistema>> PutSistemaAsync(Sistema sistema);
        Task<Sistema> GetSistemaByIdAsync(int id);
        Task<List<Sistema>> PostSistemaAsync(Sistema sistema);
        Task<List<Sistema>> DeleteSistemaAsync(int id);
    }
}
