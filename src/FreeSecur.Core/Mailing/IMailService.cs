using System.Threading.Tasks;

namespace FreeSecur.Core.Mailing
{
    public interface IMailService
    {
        Task SendMail<T>(FsMailMessage<T> fsMailMessage) where T : class;
    }
}