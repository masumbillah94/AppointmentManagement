using Domain.Dto.Common;
using Domain.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Services
{
    public static class JwtHelper
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            IEnumerable<Claim> claims = new Claim[] {
                    new Claim("UserId", userAccounts.Id.ToString()),
                    new Claim(ClaimTypes.Name, userAccounts.UserName),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddHours(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return userAccounts.GetClaims(Id);
        }

        public static UserTokens GenTokenkey(UserTokens model, JwtSettings jwtSettings)
        {
            var UserToken = new UserTokens();
            if (model == null) throw new ArgumentException(nameof(model));
            var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
            Guid Id;
            DateTime expireTime = DateTime.Now.AddHours(1);
            UserToken.Validaty = expireTime.TimeOfDay;
            var JWToken = new JwtSecurityToken(
                issuer: jwtSettings.ValidIssuer, 
                audience: jwtSettings.ValidAudience, 
                claims: model.GetClaims(out Id), 
                notBefore: new DateTimeOffset(DateTime.Now).DateTime, 
                expires: new DateTimeOffset(expireTime).DateTime, 
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );
            UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            UserToken.UserName = model.UserName;
            UserToken.Id = model.Id;
            UserToken.GuidId = Id;
            return UserToken;
        }
    }
}
