﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FreeSecur.API.Utils;
using FreeSecur.API.Core.ExceptionHandling;
using FreeSecur.API.Logic.AccessManagement;
using FreeSecur.API.Logic.AccessManagement.ErrorCodeEnums;
using FreeSecur.API.Logic.AccessManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace FreeSecur.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : FsController
    {
        private readonly AccessManagementService _accessManagementService;

        public AuthenticationController(AccessManagementService accessManagementService)
        {
            _accessManagementService = accessManagementService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, "Login token", typeof(string))]
        [SwaggerResponse(ExceptionStatusCode.ErrorCodeException, "Login error", typeof(LoginErrorCode))]
        public async Task<string> Login([FromBody] LoginModel loginModel)
        {
            var token = await _accessManagementService.Login(loginModel);

            return token;
        }
    }
}
