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
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("alexander.cheushev@gmail.com", "password"),
                Port = 465,
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("alexander.cheushev@gmail.com");
            mailMessage.To.Add(email);
            mailMessage.Body = htmlMessage;
            mailMessage.Subject = subject;


            client.Send(mailMessage);

            return Task.CompletedTask;
        }
    }
}
