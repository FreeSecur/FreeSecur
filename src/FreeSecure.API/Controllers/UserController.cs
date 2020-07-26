using FreeSecur.API.Utils;
using FreeSecur.Core;
using FreeSecur.Logic.UserLogic;
using FreeSecur.Logic.UserLogic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPut]
        public async Task<int> Create([FromBody]UserAddModel userAddModel)
        {
            var user = await _userService.Create(userAddModel);

            return user.Id;
        }
    }
}
