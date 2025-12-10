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
        #region Métodos GET
        Task<List<Rol>> GetRolesAsync();
        Task<Rol> GetRolByIdAsync(int id);
        #endregion

        #region Métodos POST
        Task<List<Rol>> PostRolAsync(Rol rol);
        #endregion

        #region Métodos PUT
        Task<List<Rol>> PutRolAsync(Rol rol);
        #endregion

        #region Métodos DELETE
        Task<List<Rol>> DeleteRolAsync(int id);
        #endregion
    }
}
