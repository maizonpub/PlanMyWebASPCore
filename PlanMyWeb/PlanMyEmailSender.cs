using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
namespace PlanMyWeb
{
    internal class PlanMyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.FromResult(0);
            //throw new System.NotImplementedException();
        }
    }
}