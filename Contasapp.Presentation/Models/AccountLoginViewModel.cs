using System.ComponentModel.DataAnnotations;

namespace Contasapp.Presentation.Models
{
    public class AccountLoginViewModel
    {
        [EmailAddress(ErrorMessage = "Informe um email válido.")]
        [Required(ErrorMessage = "Informe o email do usuário.")]
        public string? Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Informe uma senha forte com no mínimo 8 caracteres.")]
        [Required(ErrorMessage = "Informe a senha do usuário.")]
        public string? Senha { get; set; }
    }
}
