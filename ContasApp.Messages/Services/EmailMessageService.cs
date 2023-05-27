using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ContasApp.Messages.Model;

namespace ContasApp.Messages.Services
{
    public static class EmailMessageService
    {
        private const string _conta = "cotiaulajava@outlook.com";
        private const string _senha = "@Admin123456";
        private const string _smtp = "smtp-mail.outlook.com";
        private const int _port = 587;

        public static void Send(EmailMessageModel model)
        {
            var mailMessage = new MailMessage(_conta, model.EmailDestinatario);
            mailMessage.Subject = model.Assunto;
            mailMessage.Body = model.Corpo;
            mailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient(_smtp, _port);
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }
}
