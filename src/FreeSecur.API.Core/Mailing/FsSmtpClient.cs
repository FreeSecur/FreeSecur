using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FreeSecur.API.Core.Mailing
{
    public class FsSmtpClient : ISmtpClient
    {
        private readonly IOptions<FsMail> _mailOptions;

        public FsSmtpClient(IOptions<FsMail> mailOptions)
        {
            _mailOptions = mailOptions;
        }

        public async Task SendMailAsync(MailMessage message)
        {
            var settings = _mailOptions.Value;
            var smtpClient = new SmtpClient(settings.Host, settings.Port);
            await smtpClient.SendMailAsync(message);
        }
    }
}
