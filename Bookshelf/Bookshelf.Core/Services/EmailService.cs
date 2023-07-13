using System.Net.Mail;
using System.Net;

using Bookshelf.Core.Services.Contracts;

namespace Bookshelf.Core.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(List<string> receivers, string body, string subject, string smtp, string email, string password, int port)
        {
            if (receivers.Count <= 0)
            {
                return;
            }

            using (var smtpClient = new SmtpClient(smtp, port))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(email, password);
                smtpClient.EnableSsl = true;

                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(email);

                    foreach (var address in receivers)
                    {
                        mailMessage.To.Add(address);
                    }

                    mailMessage.Subject = subject;
                    mailMessage.Body = body;

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }

        }
    }
}
