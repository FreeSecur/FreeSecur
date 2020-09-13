using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace FreeSecur.API.Logic.AccessManagement
{

    public class AuthenticationService : IMiddleware, IAuthenticationService
    {

        private long? _userId;
        public bool IsAuthenticated => _userId.HasValue;

        public long UserId
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

        internal void UpdateUser(long? userId)
        {
            _userId = userId;
        }
    }
}
