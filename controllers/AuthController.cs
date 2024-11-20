using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sheridan_connect_backend.Models;
using Microsoft.Extensions.Configuration;
using sheridan_connect_backend.DB;
using sheridan_connect_backend.Handlers;
using Microsoft.Extensions.Options;

namespace sheridan_connect_backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AppSettings _appSettings;

        public class LoginDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public AuthController(UserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existingUser = await _userService.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "User already exists." });
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userService.CreateUserAsync(user);

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            Console.WriteLine(loginDto.Email + " / " + loginDto.Password);


            var existingUser = await _userService.GetUserByEmailAsync(loginDto.Email);
            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, existingUser.Password))
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, existingUser._id.ToString()),
                    new Claim(ClaimTypes.Email, existingUser.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                email = existingUser.Email
            });
        }
    }
}