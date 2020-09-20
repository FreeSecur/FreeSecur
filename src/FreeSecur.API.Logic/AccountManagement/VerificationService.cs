using FreeSecur.API.Core.Cryptography;
using FreeSecur.API.Core.ExceptionHandling.Exceptions;
using FreeSecur.API.Core.GeneralHelpers;
using FreeSecur.API.Domain;
using FreeSecur.API.Domain.Entities.Users;
using FreeSecur.API.Logic.AccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FreeSecur.API.Logic.AccountManagement
{
    /// <summary>
    /// A helper service used to create and validate verification keys for the likes of password reste and confirm email
    /// </summary>
    public class VerificationService : IVerificationService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly IFsEntityRepository _entityRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public VerificationService(
            IEncryptionService encryptionService,
            IFsEntityRepository entityRepository,
            IDateTimeProvider dateTimeProvider
            )
        {
            _encryptionService = encryptionService;
            _entityRepository = entityRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Validates wether given key is valid. If not throws status code exception bad request. If is valid returns userid
        /// </summary>
        /// <param name="key"></param>
        /// <param name="verificationType"></param>
        /// <returns>User id</returns>
        public async Task<User> ValidateVerificationKey(string key, UserVerificationType verificationType)
        {
            var decodedKey = HttpUtility.UrlDecode(key);
            var userVerificationModel = _encryptionService.DecryptModel<UserVerificationModel>(decodedKey);

            var user = await _entityRepository.GetEntity<User>(x => x.Id == userVerificationModel.UserId);

            if (user == null) throw new StatusCodeException(HttpStatusCode.BadRequest);
            if (_dateTimeProvider.Now > userVerificationModel.ExpirationDate) throw new StatusCodeException(HttpStatusCode.BadRequest);
            if (userVerificationModel.UserVerificationType != verificationType) throw new StatusCodeException(HttpStatusCode.BadRequest);

            return user;
        }

        /// <summary>
        /// Adds a unique key with expiration to the passed along URL. Mainly used to send verification URL's needed for the likes of password reset and confirm email
        /// </summary>
        /// <param name="verificationUrl"></param>
        /// <param name="user"></param>
        /// <param name="userVerificationType"></param>
        /// <returns></returns>
        public string CreateVerificationUrl(string verificationUrl, User user, UserVerificationType userVerificationType)
        {
            var verificationKey = CreateVerificationKey(user, userVerificationType);
            var verificationUrlWithKey = $"{verificationUrl}?key={verificationKey}";
            return verificationUrlWithKey;
        }

        /// <summary>
        /// Create the unique verificationKey
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userVerificationType"></param>
        /// <returns></returns>
        public string CreateVerificationKey(User user, UserVerificationType userVerificationType)
        {
            var userVerificationModel = new UserVerificationModel(user.Id, _dateTimeProvider.Now.AddHours(2), userVerificationType);
            var verificationKey = _encryptionService.EncryptModel(userVerificationModel);
            var encodedVerificationKey = HttpUtility.UrlEncode(verificationKey);
            return encodedVerificationKey;
        }
    }
}
