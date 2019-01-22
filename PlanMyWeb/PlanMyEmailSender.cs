using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
namespace PlanMyWeb
{
    internal class PlanMyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            int Port = 587;
            bool isSSL = true;
            string username = "info@maizonpub.com";
            string password = "1nf0MPL3B@";
            var fromAddress = new MailAddress("info@maizonpub.com");
            var toAddress = new MailAddress(email);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            smtp.EnableSsl = isSSL;
            try
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = htmlMessage
                }
                )
                {
                    smtp.Send(message);
                }
                return Task.FromResult(0);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            //throw new System.NotImplementedException();
        }
    }
}