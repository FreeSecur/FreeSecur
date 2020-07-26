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
    public class UserService
    {
        private readonly IFsEntityRepository _entityRepository;

        public UserService(IFsEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<User> Create(UserCreateModel userCreateModel)
        {
            var userToAdd = new User
            {
                Email = userCreateModel.Email,
                Username = userCreateModel.Username,
                FirstName = userCreateModel.FirstName,
                LastName = userCreateModel.LastName,
                Password = userCreateModel.Password
            };

            var user = await _entityRepository.AddOwner(userToAdd, null);

            return user;
        }

        public async Task<UserReadModel> Read(int id)
        {
            var user =  await _entityRepository.GetEntity<User>(user => user.Id == id);
            if (user == null)
                throw new FunctionalException($"User with id {id} does not exist", HttpStatusCode.NotFound);

            return new UserReadModel(user);
        }

        public async Task Delete(int id)
        {
            var removedUser = await _entityRepository.RemoveOwner<User>(x => x.Id == id);
            if (removedUser == null)
                throw new FunctionalException($"User with id {id} does not exist", HttpStatusCode.NotFound);
        }

        public async Task<UserReadModel> Update(int id, UserEditModel userEditModel)
        {
            var userToEdit = await _entityRepository.GetEntity<User>(x => x.Id == id);

            if (userToEdit == null)
                throw new FunctionalException($"User with id {id} does not exist", HttpStatusCode.NotFound);

            userToEdit.Email = userEditModel.Email;
            userToEdit.FirstName = userEditModel.FirstName;
            userToEdit.LastName = userEditModel.LastName;

            var updatedEntity = await _entityRepository.UpdateEntity(userToEdit, null);

            return new UserReadModel(updatedEntity);
        }
    }
}
