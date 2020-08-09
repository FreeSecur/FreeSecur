using FreeSecur.Core.Cryptography;
using FreeSecur.Core.ExceptionHandling.Exceptions;
using FreeSecur.Domain;
using FreeSecur.Domain.Entities.Users;
using FreeSecur.Logic.UserLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Logic.UserLogic
{
    public class AccountManagementService
    {
        private readonly IFsEntityRepository _entityRepository;
        private readonly IHashModule _hashModule;

        public AccountManagementService(
            IFsEntityRepository entityRepository,
            IHashModule hashModule)
        {
            _entityRepository = entityRepository;
            _hashModule = hashModule;
        }

        public async Task<User> Register(UserRegistrationModel userRegistrationModel)
        {
            var passwordHash = _hashModule.GetHash(userRegistrationModel.Password);

            var userToCreate = new User
            {
                Email = userRegistrationModel.Email,
                Username = userRegistrationModel.Username,
                FirstName = userRegistrationModel.FirstName,
                LastName = userRegistrationModel.LastName,
                Password = passwordHash,
                IsEmailConfirmed = false
            };

            var user = await _entityRepository.AddOwner(userToCreate, null);

            return user;
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
