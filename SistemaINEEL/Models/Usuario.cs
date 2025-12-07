namespace SistemaINEEL.Models
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
