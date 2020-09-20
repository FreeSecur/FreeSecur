using FreeSecur.API.Domain.Entities.Users;
using FreeSecur.API.Logic.AccountManagement.Models;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.AccountManagement
{
    public interface IVerificationService
    {
        string CreateVerificationKey(User user, UserVerificationType userVerificationType);
        string CreateVerificationUrl(string verificationUrl, User user, UserVerificationType userVerificationType);
        Task<User> ValidateVerificationKey(string key, UserVerificationType verificationType);
    }
}