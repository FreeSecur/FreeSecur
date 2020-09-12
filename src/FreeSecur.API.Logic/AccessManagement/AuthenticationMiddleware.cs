using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.AccessManagement
{

    public class AuthenticationService : IMiddleware
    {

        private int? _userId;
        public bool IsAuthenticated => _userId.HasValue;

        public int UserId
        {
            get
            {
                if (!IsAuthenticated) throw new AuthenticationException("User is not authenticated");

                return _userId.Value;
            }
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var claimPrinciple = context.User;

            var identity = claimPrinciple.Identity;

            if (identity.IsAuthenticated && identity is System.Security.Claims.ClaimsIdentity claimIdentity)
            {
                var userClaim = claimIdentity.Claims.Single(x => x.Type == "userId");
                var userId = Convert.ToInt32(userClaim.Value);
                _userId = userId;
            }

            await next(context);
        }

        internal void UpdateUser(int? userId)
        {
            _userId = userId;
        }
    }
}
