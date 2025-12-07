using Microsoft.EntityFrameworkCore;

namespace Proyecto_Ord.Models
{
    public class Consecutivo
    {
        public int ConsecutivoID { get; set; }
        public string? FolioCompleto { get; set; }
        public string? Remitente { get; set; }
        public string? Destinatario { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Asunto { get; set; }
        public int UsuarioID { get; set; }

        //ID del usuario que cancela
        public int CanceladoPor { get; set; }
        public string? MotivoCan { get; set; }
    }
}
