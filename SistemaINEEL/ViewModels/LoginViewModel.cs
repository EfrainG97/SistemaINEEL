using System.ComponentModel.DataAnnotations;

namespace SistemaINEEL.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El n�mero de empleado es requerido")]
        [Display(Name = "N�mero de Empleado")]
        public int NumEmpleado { get; set; }

        [Required(ErrorMessage = "La contrase�a es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contrase�a")]
        public string Password { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    }
}