using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sheridan_connect_backend.Models;

namespace sheridan_connect_backend.Handlers{
    public class JwtService
    {
        private readonly string _secret;

        public JwtService(IConfiguration configuration)
        {
            _secret = configuration["JwtSettings:Secret"];  // The secret key for signing JWTs
            
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user._id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddHours(1),  // Token expiry time
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}