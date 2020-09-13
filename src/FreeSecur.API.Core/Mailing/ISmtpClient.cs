using System.Net.Mail;
using System.Security;
using System.Threading.Tasks;

namespace FreeSecur.API.Core.Mailing
{
    public interface ISmtpClient
    {
        Task SendMailAsync(MailMessage message);
    }
}
