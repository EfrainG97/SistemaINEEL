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
        Task<List<Usuario>> GetUsuariosAsync();
        Task<List<Usuario>> PutUsuarioAsync(Usuario usuario);
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<List<Usuario>> PostUsuarioAsync(Usuario usuario);
        Task<List<Usuario>> DeleteUsuarioAsync(int id);
    }
}
