using FreeSecur.Core;
using FreeSecur.Core.Cryptography;
using FreeSecur.Core.ExceptionHandling.Exceptions;
using FreeSecur.Core.Mailing;
using FreeSecur.Core.Url;
using FreeSecur.Domain;
using FreeSecur.Domain.Entities.Users;
using FreeSecur.Logic.AccountManagement.Mail;
using FreeSecur.Logic.AccountManagement.MailModels;
using FreeSecur.Logic.AccountManagement.Models;
using FreeSecur.Logic.UserLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace FreeSecur.Logic.UserLogic
{
    public class AccountManagementService
    {
        private readonly IFsEntityRepository _entityRepository;
        private readonly IHashService _hashService;
        private readonly IMailService _mailService;
        private readonly IEncryptionService _encryptionService;
        private readonly FsDbContext _dbContext;

        public AccountManagementService(
            IFsEntityRepository entityRepository,
            IHashService hashService,
            IMailService mailService,
            IEncryptionService encryptionService, 
            FsDbContext dbContext)
        {
            _entityRepository = entityRepository;
            _hashService = hashService;
            _mailService = mailService;
            _encryptionService = encryptionService;
            _dbContext = dbContext;
        }

        public async Task ConfirmEmail(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new FunctionalException("Invalid key", HttpStatusCode.BadRequest);
            }

            var decodedKey = HttpUtility.UrlDecode(key);
            var userReadModel = _encryptionService.DecryptModel<UserReadModel>(decodedKey);

            if (!userReadModel.Id.HasValue)
            {
                throw new FunctionalException("Invalid key", HttpStatusCode.BadRequest);
            }

            var user = await _entityRepository.GetEntity<User>(x => x.Id == userReadModel.Id.Value);
            if (user == null)
            {
                throw new FunctionalException("Invalid key", HttpStatusCode.BadRequest);
            }

            user.IsEmailConfirmed = true;

            await _entityRepository.UpdateEntity(user, null);
        }

        public async Task<UserRegistrationResponseModel> Register(UserRegistrationModel userRegistrationModel)
        {
            var passwordHash = _hashService.GetHash(userRegistrationModel.Password);

            //Prepare user model
            var userToCreate = new User
            {
                Email = userRegistrationModel.Email,
                Username = userRegistrationModel.Username,
                FirstName = userRegistrationModel.FirstName,
                LastName = userRegistrationModel.LastName,
                Password = passwordHash,
                IsEmailConfirmed = false
            };
            

            //Execute saving and send mails
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var user = await _entityRepository.AddOwner(userToCreate, null);
                try
                {
                    var confirmationKey = await SendConfirmEmailMail(userToCreate, userRegistrationModel.ConfirmationUrl);

                    await transaction.CommitAsync();

                    return new UserRegistrationResponseModel(user.Id, confirmationKey);
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        /// <summary>
        /// Return the key that has been send
        /// </summary>
        /// <param name="userToCreate"></param>
        /// <param name="confirmationUrl"></param>
        /// <returns></returns>
        public async Task<string> SendConfirmEmailMail(User userToCreate, string confirmationUrl)
        {
            var userReadModel = new UserReadModel(userToCreate);
            var confirmationKey = _encryptionService.EncryptModel(userReadModel);
            var encodedConfirmationKey = HttpUtility.UrlEncode(confirmationKey);

            var confirmationUrlWithKey = $"{confirmationUrl}?key={encodedConfirmationKey}";
            var confirmationMailModel = new ConfirmationMailModel(confirmationUrlWithKey, userToCreate.FirstName, userToCreate.LastName);
            var message = new FsMailMessage<ConfirmationMailModel>(userToCreate.Email, MailResources.ConfirmEmail_Subject, MailResources.ConfirmEmail_Body, confirmationMailModel);

            await _mailService.SendMail(message);

            return encodedConfirmationKey;
        }

        public async Task<UserReadModel> UpdatePersonalData(int id, UserUpdateModel userUpdateModel)
        {
            var userToEdit = await _entityRepository.GetEntity<User>(x => x.Id == id);

            if (userToEdit == null)
                throw new FunctionalException($"User with id {id} does not exist", HttpStatusCode.NotFound);

            userToEdit.Email = userUpdateModel.Email;
            userToEdit.FirstName = userUpdateModel.FirstName;
            userToEdit.LastName = userUpdateModel.LastName;

            var updatedEntity = await _entityRepository.UpdateEntity(userToEdit, null);

            return new UserReadModel(updatedEntity);
        }
    }
}
