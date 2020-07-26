using FreeSecur.API.Utils;
using FreeSecur.Core;
using FreeSecur.Logic.UserLogic;
using FreeSecur.Logic.UserLogic.Models;
using FreeSecure.API.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FreeSecure.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : FsController
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, "Created user id", typeof(int))]
        public async Task<int> Create([FromBody] UserCreateModel userAddModel)
        {
            var user = await _userService.Create(userAddModel);

            return user.Id;
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns the user", typeof(UserReadModel))]
        public async Task<UserReadModel> Read([FromRoute] int id)
        {
            var user = await _userService.Read(id);

            return user;
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Deleted user id", typeof(int))]
        public async Task<int> Delete([FromRoute] int id)
        {
            await _userService.Delete(id);
            return id;
        }

        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Updated user model", typeof(UserReadModel))]
        public async Task<UserReadModel> Update([FromRoute] int id, [FromBody] UserEditModel userEditModel)
        {
            var updatedUser = await _userService.Update(id, userEditModel);

            return updatedUser;
        }
    }
}
