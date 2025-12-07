using Microsoft.EntityFrameworkCore;

namespace SistemaINEEL.Models
{
    public class Reporte
    {
        public int ReporteID { get; set; }
        public int ConsecutivoID { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Asunto { get; set; }

        //ID del usuario que crea el reporte
        public int Registro { get; set; }

    }
}
