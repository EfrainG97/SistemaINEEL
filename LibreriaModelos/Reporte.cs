using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaModelos
{
    public class Reporte
    {
        public int ReporteID { get; set; }

        [Required(ErrorMessage = "El consecutivo es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un consecutivo válido")]
        [Display(Name = "Consecutivo")]
        public int ConsecutivoID { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "El asunto es requerido")]
        [StringLength(500, ErrorMessage = "El asunto no puede exceder 500 caracteres")]
        [Display(Name = "Asunto")]
        public string? Asunto { get; set; }

        //ID del usuario que crea el reporte
        [Required(ErrorMessage = "El usuario que registra es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un usuario válido")]
        [Display(Name = "Registrado Por")]
        public int Registro { get; set; }
    }
}
