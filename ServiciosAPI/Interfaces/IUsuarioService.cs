using LibreriaModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosAPI.Interfaces
{
    public interface IUsuarioService
    {
        #region Métodos GET
        Task<List<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int id);
        #endregion

        #region Métodos POST
        Task<List<Usuario>> PostUsuarioAsync(Usuario usuario);
        #endregion

        #region Métodos PUT
        Task<List<Usuario>> PutUsuarioAsync(Usuario usuario);
        #endregion

        #region Métodos DELETE
        Task<List<Usuario>> DeleteUsuarioAsync(int id);
        #endregion
    }
}
