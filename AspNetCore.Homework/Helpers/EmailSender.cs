using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AspNetCore.Homework.Helpers
{
    public class EmailSender: IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient client = new SmtpClient("smtp.mail.ru")
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("alex.cheushev@mail.ru", "password"),
                Port = 587,
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("alex.cheushev@mail.ru")
            };

            mailMessage.To.Add(email);
            mailMessage.Body = htmlMessage;
            mailMessage.Subject = subject;


            client.Send(mailMessage);

            return Task.CompletedTask;
        }
    }
}
