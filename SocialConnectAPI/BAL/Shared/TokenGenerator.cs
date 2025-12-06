
using Microsoft.IdentityModel.Tokens;
using MODEL.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BAL.Shared
{
    public static class TokenGenerator
    {
        public static string GenerateCustomerToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("^6L@QAWsW>eUk/q|X38fHu?#=6YTWWZWXw[0T/C^O84R48NC@9p{1,~)M?2s{so"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

            var token = new JwtSecurityToken(
                issuer: "SocialConnectAPI",
                audience: "SocialConnectAPI",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
