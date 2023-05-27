using System.ComponentModel.DataAnnotations;

namespace Contasapp.Presentation.Models
{
    public class AccountRegisterViewModel
    {
        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracter.")]
        [MaxLength(150, ErrorMessage = "Informe no máximo {1} caracter.")]
        [Required(ErrorMessage = "Informe o nome do usuário.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Informe um email válido.")]
        [Required(ErrorMessage = "Informe o email do usuário.")]
        public string? Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Informe uma senha forte com no mínimo 8 caracteres.")]
        [Required(ErrorMessage = "Informe a senha do usuário.")]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem.")]
        [Required(ErrorMessage = "Confirme a a senha do usuário.")]
        public string? SenhaConfirmacao { get; set; }
    }
}
