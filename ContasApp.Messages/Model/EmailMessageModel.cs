using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Messages.Model
{
    public class EmailMessageModel
    {
        public string? EmailDestinatario { get; set; }
        public string? Assunto { get; set; }
        public string? Corpo { get; set; }
    }
}

