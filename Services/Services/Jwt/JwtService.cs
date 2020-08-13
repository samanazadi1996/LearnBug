using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services
{
    public class JwtService : IJwtService
    {
        private readonly IUserRepository _userRepository;

        public JwtService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string GenerateAsync(LoginUserViewModel user)
        {
            var issuer = ConfigurationManager.AppSettings["Issuer"];
            var audienceId = ConfigurationManager.AppSettings["AudienceId"];
            var audienceSecret =Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["AudienceSecret"]);

            var secretKey = audienceSecret; // longer that 16 character
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);


            var claims = _getClaimsAsync(user);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audienceId,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(0),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;

        }
        private IEnumerable<Claim> _getClaimsAsync(LoginUserViewModel user)
        {
            var password = user.Password.Encrypt();
            var person = _userRepository.Where(p => p.Username == user.Username.Trim() && p.Password == password && p.IsActive)
                .FirstOrDefault();
            var list = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,person.Name),
                new Claim(ClaimTypes.Role,person.Roles),
                new Claim(ClaimTypes.NameIdentifier,person.Id.ToString())
            };
            return list;
        }
    }
}
