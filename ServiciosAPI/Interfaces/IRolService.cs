using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaModelos;

namespace ServiciosAPI.Interfaces
{
    public interface IRolService
    {
        Task<List<Rol>> GetRolesAsync();
        Task<List<Rol>> PutRolAsync(Rol rol);
        Task<Rol> GetRolByIdAsync(int id);
        Task<List<Rol>> PostRolAsync(Rol rol);
        Task<List<Rol>> DeleteRolAsync(int id);
    }
}
