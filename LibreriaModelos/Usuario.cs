using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaModelos
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public int NumEmpleado { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Password { get; set; }
        public int IDRol { get; set; }
    }
}
