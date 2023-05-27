using System.ComponentModel.DataAnnotations;

namespace Contasapp.Presentation.Models
{
    public class ContasEdicaoViewModel
    {
        public Guid? Id { get; set; }

        [MaxLength(150, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o nome da conta.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Informe a data da conta.")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Informe o valor da conta.")]
        public decimal? Valor { get; set; }

        [MaxLength(250, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Informe as observações da conta.")]
        public string? Observacoes { get; set; }

        [Required(ErrorMessage = "Selecione a categoria da conta.")]
        public Guid? CategoriaId { get; set; }
    }
}
