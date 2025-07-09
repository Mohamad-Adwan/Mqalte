using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ArticlProject.Code
{
    public class EmailSender : IEmailSender
    {
        public  Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mohammedadwan44@gmail.com", "xpzd droe sbyp cjbf"),
                EnableSsl = true,


            };
            return smtpClient.SendMailAsync("mohammedadwan44@gmail.com", email, subject, htmlMessage);
        }
    }
}
