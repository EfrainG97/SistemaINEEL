using System.ComponentModel.DataAnnotations;

namespace SistemaINEEL.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El numero de empleado es requerido")]
        [Display(Name = "Numero de Empleado")]
        public int? NumEmpleado { get; set; }

        [Required(ErrorMessage = "La contrasena es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contrasena")]
        public string Password { get; set; } = string.Empty;
        
        public string? ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    }
}