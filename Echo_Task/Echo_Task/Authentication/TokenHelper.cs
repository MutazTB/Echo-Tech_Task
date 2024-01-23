using Domain.Security;
using Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Echo_TaskAPI.Authentication
{
    public class TokenHelper
    {
        public static string GenerateToken(SecurityData securityData)
        {
            if (securityData == null)
            {
                return "";
            }
            var claims = new List<Claim> 
            {
                new Claim("UserName", securityData.UserName),
                new Claim("UserId", securityData.UserId),
                new Claim("UserEmail", securityData.UserEmail),
            };
            if(securityData.UserRoles != null)
            {
                claims.AddRange(securityData.UserRoles.Select(role => new Claim(ClaimTypes.Role, role)));
            }

            var returnToken = new JwtSecurityToken
                (
                issuer : JWT_Secret.Issuer,
                audience: JWT_Secret.Audience,
                claims: claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_Secret.Key)), SecurityAlgorithms.HmacSha256)
                );
            return Common.CompressString(new JwtSecurityTokenHandler().WriteToken(returnToken));
        }

        public static void SetTokenInCookie(HttpResponse httpResponse ,string token) 
        {            
           
        }
    }
}
