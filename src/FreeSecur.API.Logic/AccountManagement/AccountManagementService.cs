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
using System.Web;
using FreeSecur.API.Logic.AccessManagement;

namespace FreeSecur.API.Logic.UserLogic
{
    public class AccountManagementService
    {
        private readonly IFsEntityRepository _entityRepository;
        private readonly IHashService _hashService;
        private readonly IMailService _mailService;
        private readonly IEncryptionService _encryptionService;
        private readonly FsDbContext _dbContext;
        private readonly IAuthenticationService _authenticationService;

        public AccountManagementService(
            IFsEntityRepository entityRepository,
            IHashService hashService,
            IMailService mailService,
            IEncryptionService encryptionService, 
            FsDbContext dbContext,
            IAuthenticationService authenticationService)
        {
            _entityRepository = entityRepository;
            _hashService = hashService;
            _mailService = mailService;
            _encryptionService = encryptionService;
            _dbContext = dbContext;
            _authenticationService = authenticationService;
        }

        public async Task ConfirmEmail(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new StatusCodeException(HttpStatusCode.BadRequest);

            var decodedKey = HttpUtility.UrlDecode(key);
            var userReadModel = _encryptionService.DecryptModel<UserReadModel>(decodedKey);

            if (!userReadModel.Id.HasValue) throw new StatusCodeException(HttpStatusCode.BadRequest);

            var user = await _entityRepository.GetEntity<User>(x => x.Id == userReadModel.Id.Value);

            if (user == null) throw new StatusCodeException(HttpStatusCode.BadRequest);

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
            var message = new MailMessage<ConfirmationMailModel>(userToCreate.Email, MailResources.ConfirmEmail_Subject, MailResources.ConfirmEmail_Body, confirmationMailModel);

            await _mailService.SendMail(message);

            return encodedConfirmationKey;
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
            userToEdit.FirstName = string.IsNullOrWhiteSpace(userUpdateModel.FirstName) ? userToEdit.FirstName: userUpdateModel.FirstName;
            userToEdit.LastName = string.IsNullOrWhiteSpace(userUpdateModel.LastName) ? userToEdit.LastName : userUpdateModel.LastName;

            var updatedEntity = await _entityRepository.UpdateEntity(userToEdit, null);

            return new UserReadModel(updatedEntity);
        }
    }
}
