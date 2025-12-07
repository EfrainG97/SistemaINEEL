namespace Proyecto_Ord.Models
{
    public class Auditoria
    {
        public int AuditoriaID { get; set; }
        public int UsuarioID { get; set; }
        public int ConsecutivoID { get; set; }
        public string? Accion { get; set; }
        public DateTime FechaHora { get; set; }
        public string? Comentario { get; set; }
    }
}
