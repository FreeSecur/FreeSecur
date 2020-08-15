using FreeSecur.Core.Reflection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Core.Mailing
{
    public class MailService : IMailService
    {
        private readonly IOptionsSnapshot<FsMail> _fsMailOptions;
        private readonly StringInterpolationService _stringInterpolationService;

        public MailService(
            IOptionsSnapshot<FsMail> fsMailOptions,
            StringInterpolationService stringInterpolationService)
        {
            _fsMailOptions = fsMailOptions;
            _stringInterpolationService = stringInterpolationService;
        }

        public async Task SendMail<T>(FsMailMessage<T> fsMailMessage)
            where T : class
        {
            var fsMailSettings = _fsMailOptions.Value;
            var smtpClient = new SmtpClient(fsMailSettings.Host, fsMailSettings.Port);

            var message = new MailMessage();
            message.To.AddRange(fsMailMessage.ToAddresses.ToArray());
            message.From = new MailAddress(fsMailSettings.FromAddress);

            var subject = _stringInterpolationService.Interpolate(fsMailMessage.Subject, fsMailMessage.Model);
            var body = _stringInterpolationService.Interpolate(fsMailMessage.Body, fsMailMessage.Model);

            message.Subject = subject;
            message.Body = body;

            await smtpClient.SendMailAsync(message);
        }
    }
}
