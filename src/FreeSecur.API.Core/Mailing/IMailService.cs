using System.Threading.Tasks;

namespace FreeSecur.API.Core.Mailing
{
    public interface IMailService
    {
        Task SendMail<T>(MailMessage<T> fsMailMessage) where T : class;
    }
}