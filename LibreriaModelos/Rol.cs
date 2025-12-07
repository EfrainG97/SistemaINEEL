using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaModelos
{
    public class Rol
    {
        public int RolID { get; set; }

        [Required(ErrorMessage = "El nombre del rol es requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre del rol debe tener entre 2 y 50 caracteres")]
        [Display(Name = "Nombre del Rol")]
        public string? NombreRol { get; set; }
    }
}
