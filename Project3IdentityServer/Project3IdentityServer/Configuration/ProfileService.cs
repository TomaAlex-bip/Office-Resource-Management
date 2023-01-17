using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Project3Api.Core;
using Project3Api.Core.Repositories;
using System.Security.Claims;

namespace Project3IdentityServer.Configuration
{
    public class ProfileService : IProfileService
    {
        private const string USER_ROLE = "user";
        private const string ADMIN_ROLE = "admin";

        private readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userRepository.GetUserAsync(Guid.Parse(sub));
            if (user == null)
                return;

            var claims = new List<Claim>
            {
                new Claim("role", user.Role == 1 ? ADMIN_ROLE : USER_ROLE),
                new Claim("username", user.Username)
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userRepository.GetUserAsync(Guid.Parse(sub));
            context.IsActive = user != null;
        }
    }
}
