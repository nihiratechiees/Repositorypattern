using Microsoft.IdentityModel.Tokens;
using Repopattern.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Repopattern.Service
{
    public class JWTService
    {
        private readonly IConfiguration iconfig;
        public JWTService(IConfiguration configuration) { 
        this.iconfig = configuration;
        }

        public string GenerateToken(User user)
        {
            var TokenHandler=new JwtSecurityTokenHandler();

            var claim = new[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.UserRole)
            };

            string _key = iconfig["Jwtsettings:key"];

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_key)
                );

            var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: iconfig["Jwtsettings:Issuer"],
                audience: iconfig["Jwtsettings:Audience"],
                claims: claim,
                signingCredentials: cred,
                expires:DateTime.Now.AddMinutes(30)
                );


            return TokenHandler.WriteToken(token);
        }

    }
}
