using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaModelos
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
