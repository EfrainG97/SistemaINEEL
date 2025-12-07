using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaModelos
{
    public class Consecutivo
    {
        public int ConsecutivoID { get; set; }

        [StringLength(50, ErrorMessage = "El folio completo no puede exceder 50 caracteres")]
        [Display(Name = "Folio Completo")]
        public string? FolioCompleto { get; set; }

        [Required(ErrorMessage = "El remitente es requerido")]
        [StringLength(200, ErrorMessage = "El remitente no puede exceder 200 caracteres")]
        [Display(Name = "Remitente")]
        public string? Remitente { get; set; }

        [Required(ErrorMessage = "El destinatario es requerido")]
        [StringLength(200, ErrorMessage = "El destinatario no puede exceder 200 caracteres")]
        [Display(Name = "Destinatario")]
        public string? Destinatario { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "El asunto es requerido")]
        [StringLength(500, ErrorMessage = "El asunto no puede exceder 500 caracteres")]
        [Display(Name = "Asunto")]
        public string? Asunto { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un usuario válido")]
        [Display(Name = "Usuario")]
        public int UsuarioID { get; set; }

        //ID del usuario que cancela
        [Display(Name = "Cancelado Por")]
        public int CanceladoPor { get; set; }

        [StringLength(500, ErrorMessage = "El motivo de cancelación no puede exceder 500 caracteres")]
        [Display(Name = "Motivo de Cancelación")]
        public string? MotivoCan { get; set; }
    }
}
