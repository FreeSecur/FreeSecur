using FreeSecur.Domain;
using FreeSecur.Domain.Entities.Users;
using FreeSecur.Logic.UserLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<User> Create(UserAddModel userAddModel)
        {
            var userToAdd = new User
            {
                Email = userAddModel.Email,
                FirstName = userAddModel.FirstName,
                LastName = userAddModel.LastName,
                Password = userAddModel.Password
            };

            var user = await _entityRepository.AddOwner(userToAdd, null);

            return user;
        }
    }
}
