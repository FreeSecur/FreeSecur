using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.AccessManagement
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }
        int UserId { get; }
    }
}