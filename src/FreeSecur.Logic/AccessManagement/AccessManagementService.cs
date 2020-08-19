﻿using FreeSecur.Core.Cryptography;
using FreeSecur.Core.ExceptionHandling.Exceptions;
using FreeSecur.Domain;
using FreeSecur.Domain.Entities.Users;
using FreeSecur.Logic.AccessManagement.ErrorCodeEnums;
using FreeSecur.Logic.AccessManagement.Models;
using FreeSecur.Logic.UserLogic.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Logic.AccessManagement
{
    public class AccessManagementService
    {
        private readonly IFsEntityRepository _fsEntityRepository;
        private readonly IHashService _hashService;
        private readonly FsJwtAuthentication _jwtSettings;

        public AccessManagementService(
            IFsEntityRepository fsEntityRepository, 
            IHashService hashService,
            IOptions<FsJwtAuthentication> jwtOptions)
        {
            _fsEntityRepository = fsEntityRepository;
            _hashService = hashService;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var user = await _fsEntityRepository.GetEntity<User>(x => x.Email == loginModel.Username || x.Username == loginModel.Username);

            if (user == null) throw new ErrorCodeException(LoginErrorCode.InvalidCredentials);

            var passwordIsCorrect = _hashService.Verify(loginModel.Password, user.Password);

            if (!passwordIsCorrect) throw new ErrorCodeException(LoginErrorCode.InvalidCredentials);

            if (!user.IsEmailConfirmed) throw new ErrorCodeException(LoginErrorCode.EmailNotConfirmed);

            var token = GenerateJWTToken(user);

            return token;
        }

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userReadModel = new UserReadModel(user);

            var tokenClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("fullName", userReadModel.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var tokenData = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: tokenClaims,
                expires: DateTime.Now.AddSeconds(_jwtSettings.Expiry),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.WriteToken(tokenData);

            return token;
        }
    }
}
