using Contasapp.Presentation.Models;
using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class ContasConsultaViewModel
    {
        [Required(ErrorMessage = "Informe a data inicial.")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "Informe a data final.")]
        public DateTime? DataFim { get; set; }

        public List<ContasConsultaResultadoViewModel>? Resultado { get; set; }
    }
}

