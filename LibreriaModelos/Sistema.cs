using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaModelos
{
    public class Sistema
    {
        public int SistemaID { get; set; }

        [Required(ErrorMessage = "La gerencia es requerida")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "La gerencia debe tener entre 2 y 100 caracteres")]
        [Display(Name = "Gerencia")]
        public string? Gerencia { get; set; }
    }
}
