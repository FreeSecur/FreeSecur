using System.Net.Mail;

namespace FreeSecur.Core.Mailing
{
    public static class MailExtensions
    {
        public static MailAddressCollection AddRange(this MailAddressCollection mailCollection, params string[] mailAddresses)
        {
            foreach(var mailAddress in mailAddresses)
            {
                var mailAddressObject = new MailAddress(mailAddress);
                mailCollection.Add(mailAddressObject);
            }

            return mailCollection;
        }
    }
}
