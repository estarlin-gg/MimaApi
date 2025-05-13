using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mima.Application.Dtos;
using Mima.Application.Services.Abstraction;
using Mima.Domain.Model;

namespace Mima.Application.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Login(LoginDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, userDto.Password, false, false);
            if (!result.Succeeded) return null;

            var userMapped = _mapper.Map<UserDto>(user);
            Console.WriteLine(userMapped);

            var token = GenerateToken(user);

            return new AuthResponse
            {
                User = userMapped,
                Token = token
            };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthResponse> Register(RegisterDto userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded) return null;

            var token = GenerateToken(user);
            var userMapped = _mapper.Map<UserDto>(user);
            Console.WriteLine(userMapped);

            return new AuthResponse
            {
                User = userMapped,
                Token = token
            };
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("userId", user.Id),
                new Claim("email", user.Email),
                new Claim("username", user.UserName),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                 new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMonths(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
