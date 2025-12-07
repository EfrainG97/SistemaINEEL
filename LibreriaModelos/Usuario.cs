using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaModelos
{
    public class Usuario
    {
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "El número de empleado es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de empleado debe ser mayor a 0")]
        [Display(Name = "Número de Empleado")]
        public int NumEmpleado { get; set; }

        [StringLength(100, ErrorMessage = "El nombre de usuario no puede exceder 100 caracteres")]
        [Display(Name = "Nombre de Usuario")]
        public string? NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 4 y 50 caracteres")]
        [Display(Name = "Contraseña")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un rol válido")]
        [Display(Name = "Rol")]
        public int IDRol { get; set; }
    }
}
