using System;
using System.Collections.Generic;

namespace FreeSecur.Core.Mailing
{
    public class MailMessage<T>
        where T : class
    {
        public MailMessage(string toAddress, string subject, string template, T model)
        {
            if (string.IsNullOrWhiteSpace(toAddress))
                throw new ArgumentNullException(nameof(toAddress));

            ToAddresses = new List<string> { toAddress };
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Body = template ?? throw new ArgumentNullException(nameof(template));
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public MailMessage(List<string> toAddresses, string subject, string template, T model)
        {
            ToAddresses = toAddresses ?? throw new ArgumentNullException(nameof(toAddresses));
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Body = template ?? throw new ArgumentNullException(nameof(template));
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public List<string> ToAddresses { get; }
        public string Subject { get; }
        public string Body { get; }
        public T Model { get; }

    }
}
