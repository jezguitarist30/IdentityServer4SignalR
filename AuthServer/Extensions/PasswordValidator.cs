using BAL.Services.Interfaces;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer.Extensions
{
    public class PasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserService _userService;

        public PasswordValidator(IUserService userService)
        {
            _userService = userService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var userName = context.UserName;
            var password = context.Password;

            var isUserExist = await _userService.IsUserCredentialsValidAsync(userName, password);

            // validate username/password against in-memory store
            if (isUserExist)
            {
                var user = _userService.GetUserByUsername(userName);

                context.Result = new GrantValidationResult(
                  subject: user.Id.ToString(),
                  authenticationMethod: "",
                  claims: new[] {
                      new Claim("name", user.Name),
                      new Claim("email", user.Email),
                      new Claim("userId", user.Id.ToString())
                  });
            }
        }
    }
}
