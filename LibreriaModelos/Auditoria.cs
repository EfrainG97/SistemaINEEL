using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaModelos
{
    public class Auditoria
    {
        public int AuditoriaID { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un usuario válido")]
        [Display(Name = "Usuario")]
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "El consecutivo es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un consecutivo válido")]
        [Display(Name = "Consecutivo")]
        public int ConsecutivoID { get; set; }

        [Required(ErrorMessage = "La acción es requerida")]
        [StringLength(50, ErrorMessage = "La acción no puede exceder 50 caracteres")]
        [Display(Name = "Acción")]
        public string? Accion { get; set; }

        [Required(ErrorMessage = "La fecha y hora es requerida")]
        [Display(Name = "Fecha y Hora")]
        [DataType(DataType.DateTime)]
        public DateTime FechaHora { get; set; }

        [StringLength(1000, ErrorMessage = "El comentario no puede exceder 1000 caracteres")]
        [Display(Name = "Comentario")]
        public string? Comentario { get; set; }
    }
}
