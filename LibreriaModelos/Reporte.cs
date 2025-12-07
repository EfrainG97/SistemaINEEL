using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaModelos
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
