using System.ComponentModel.DataAnnotations;

namespace Proyecto_Ord.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El número de empleado es requerido")]
        [Display(Name = "Número de Empleado")]
        public int NumEmpleado { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    }
}