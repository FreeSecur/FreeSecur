using FreeSecur.API.Logic.AccountManagement.Mail;
using FreeSecur.API.Core.Cryptography;
using FreeSecur.API.Core.ExceptionHandling.Exceptions;
using FreeSecur.API.Core.Mailing;
using FreeSecur.API.Domain;
using FreeSecur.API.Domain.Entities.Users;
using FreeSecur.API.Logic.AccountManagement.Models;
using FreeSecur.API.Logic.UserLogic.Models;
using System.Net;
using System.Threading.Tasks;
using FreeSecur.API.Logic.AccessManagement;
using FreeSecur.API.Core.GeneralHelpers;

namespace FreeSecur.API.Logic.AccountManagement
{
    public class AccountManagementService
    {
        private readonly IFsEntityRepository _entityRepository;
        private readonly IHashService _hashService;
        private readonly IMailService _mailService;
        private readonly FsDbContext _dbContext;
        private readonly IAuthenticationService _authenticationService;
        private readonly IVerificationService _verificationService;

        public AccountManagementService(
            IFsEntityRepository entityRepository,
            IHashService hashService,
            IMailService mailService,
            FsDbContext dbContext,
            IAuthenticationService authenticationService,
            IVerificationService verificationService
            )
        {
            _entityRepository = entityRepository;
            _hashService = hashService;
            _mailService = mailService;
            _dbContext = dbContext;
            _authenticationService = authenticationService;
            _verificationService = verificationService;
        }

        public async Task ConfirmEmail(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new StatusCodeException(HttpStatusCode.BadRequest);
            var user = await _verificationService.ValidateVerificationKey(key, UserVerificationType.ConfirmEmail);
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
                Password = passwordHash.Hash,
                PasswordSalt = passwordHash.Salt,
                IsEmailConfirmed = false
            };


            //Execute saving and send mails
            using var transaction = _dbContext.Database.BeginTransaction();
            var user = await _entityRepository.AddOwner(userToCreate, null);
            try
            {
                await SendConfirmEmailMail(userToCreate, userRegistrationModel.ConfirmationUrl);
                await transaction.CommitAsync();

                return new UserRegistrationResponseModel(user.Id);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task ResetPassword(UserPasswordResetModel userPasswordResetModel)
        {
            var user = await _verificationService.ValidateVerificationKey(userPasswordResetModel.Key, UserVerificationType.PasswordReset);
            if (user.Email != userPasswordResetModel.Username && user.Username != userPasswordResetModel.Username) throw new StatusCodeException(HttpStatusCode.BadRequest);

            var passwordHashResult = _hashService.GetHash(userPasswordResetModel.Password);

            user.Password = passwordHashResult.Hash;
            user.PasswordSalt = passwordHashResult.Salt;

            await _entityRepository.UpdateEntity(user, null);
        }

        public async Task RequestPasswordReset(UserRequestPasswordResetModel resetModel)
        {
            var user = await _entityRepository.GetEntity<User>(x => x.Email == resetModel.Username || x.Username == resetModel.Username);

            //Just return if user is null, dont throw any exceptions as this could be used as indication wether user exists or not
            if (user == null) return;

            var passwordResetUrlWithKey = _verificationService.CreateVerificationUrl(resetModel.PasswordResetUrl, user, UserVerificationType.PasswordReset);
            var passwordResetMailModel = new PasswordResetMailModel(user.FirstName, user.LastName, passwordResetUrlWithKey);
            var message = new MailMessage<PasswordResetMailModel>(user.Email, MailResources.PasswordResetMail_Subject, MailResources.PasswordResetMail_Body, passwordResetMailModel);

            await _mailService.SendMail(message);
        }

        public async Task SendConfirmEmailMail(User user, string confirmationUrl)
        {
            var confirmationUrlWithKey = _verificationService.CreateVerificationUrl(confirmationUrl, user, UserVerificationType.ConfirmEmail);
            var confirmationMailModel = new ConfirmationMailModel(confirmationUrlWithKey, user.FirstName, user.LastName);
            var message = new MailMessage<ConfirmationMailModel>(user.Email, MailResources.ConfirmEmail_Subject, MailResources.ConfirmEmail_Body, confirmationMailModel);

            await _mailService.SendMail(message);
        }

        /// <summary>
        /// Updates personal data  based on given model for the currently logged in user
        /// </summary>
        /// <param name="userUpdateModel"></param>
        /// <returns></returns>
        public async Task<UserReadModel> UpdatePersonalData(UserUpdateModel userUpdateModel)
        {
            if (!_authenticationService.IsAuthenticated) throw new StatusCodeException(HttpStatusCode.Unauthorized);
            var userId = _authenticationService.UserId;

            var userToEdit = await _entityRepository.GetEntity<User>(x => x.Id == userId);

            userToEdit.Email = string.IsNullOrWhiteSpace(userUpdateModel.Email) ? userToEdit.Email : userUpdateModel.Email;
            userToEdit.FirstName = string.IsNullOrWhiteSpace(userUpdateModel.FirstName) ? userToEdit.FirstName : userUpdateModel.FirstName;
            userToEdit.LastName = string.IsNullOrWhiteSpace(userUpdateModel.LastName) ? userToEdit.LastName : userUpdateModel.LastName;

            var updatedEntity = await _entityRepository.UpdateEntity(userToEdit, null);

            return new UserReadModel(updatedEntity);
        }
    }
}
