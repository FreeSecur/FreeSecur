using FreeSecur.API.Core.Reflection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Core.Mailing
{
    public class MailService : IMailService
    {
        private readonly IOptions<FsMail> _fsMailOptions;
        private readonly StringInterpolationService _stringInterpolationService;
        private readonly ISmtpClient _smtpClient;

        public MailService(
            IOptions<FsMail> fsMailOptions,
            StringInterpolationService stringInterpolationService,
            ISmtpClient smtpClient)
        {
            _fsMailOptions = fsMailOptions;
            _stringInterpolationService = stringInterpolationService;
            _smtpClient = smtpClient;
        }

        public async Task SendMail<T>(MailMessage<T> fsMailMessage)
            where T : class
        {
            var fsMailSettings = _fsMailOptions.Value;

            var message = new MailMessage();
            message.To.AddRange(fsMailMessage.ToAddresses.ToArray());
            message.From = new MailAddress(fsMailSettings.FromAddress);

            var subject = _stringInterpolationService.Interpolate(fsMailMessage.Subject, fsMailMessage.Model);
            var body = _stringInterpolationService.Interpolate(fsMailMessage.Body, fsMailMessage.Model);

            message.Subject = subject;
            message.Body = body;

            await _smtpClient.SendMailAsync(message);
        }
    }
}
