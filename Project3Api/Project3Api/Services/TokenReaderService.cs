using Project3Api.Core.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Project3Api.Services
{
    public class TokenReaderService : ITokenReaderService
    {
        public Guid GetUserId(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler
                .ReadToken(token.Replace("Bearer ", ""));

            Guid userId = Guid.Empty;
            string? subId = securityToken.Claims
                .FirstOrDefault(c => c.Type.Equals("sub"))?.Value;
            if (subId != null)
                userId = Guid.Parse(subId);

            return userId;
        }
    }
}
