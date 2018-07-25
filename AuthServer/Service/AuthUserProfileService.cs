using DAL.Repositories.Interfaces;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer.Service
{
    public class AuthUserProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public AuthUserProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            Guid userId = Guid.Parse(subjectId);

            var userClaims = _userRepository.GetUserClaimsByUserId(userId);

            context.IssuedClaims = userClaims.Select(_ =>
                new Claim(_.ClaimType, _.ClaimValue))?.ToList();

            context.IssuedClaims.Add(new Claim("userId", userId.ToString()));

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            Guid userId = Guid.Parse(subjectId);

            context.IsActive = _userRepository.IsUserActive(userId);

            return Task.FromResult(0);
        }
    }
}
