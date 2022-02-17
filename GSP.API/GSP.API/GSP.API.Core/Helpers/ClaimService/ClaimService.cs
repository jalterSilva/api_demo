using GSP.API.Core.Models.Authentication;
using GSP.API.Core.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GSP.API.Core.Helpers.ClaimService
{
    public class ClaimService : IClaimService
    {
        #region Properties
        private AppSettings _appSettings;
        #endregion

        #region Constructor
        public ClaimService([FromServices] IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        #endregion

        #region Create Token
        public TokenResponseModel CreateToken(UserModel user)
        {
            List<Claim> authClaims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("CurrentHerdCode", user?.CurrentHerdCode ?? ""),
                new Claim("UserRoleId", user.UserRole.UserRoleId.ToString() ?? ""),
                new Claim(ClaimTypes.Role, user.UserRole.RoleName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));

            var token = new JwtSecurityToken(
                issuer: string.Empty,
                audience: string.Empty,
                expires: DateTime.Now.AddDays(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new TokenResponseModel(new JwtSecurityTokenHandler().WriteToken(token), "Bearer", "");
        }
        #endregion
    }
}
