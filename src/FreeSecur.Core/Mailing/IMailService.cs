using System.Threading.Tasks;

namespace FreeSecur.Core.Mailing
{
    public interface IMailService
    {
        Task SendMail<T>(MailMessage<T> fsMailMessage) where T : class;
    }
}