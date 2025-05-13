using Microsoft.AspNetCore.Identity;
using Mima.Domain.Model;
using Mima.Infrastructure.Repositories.Abstraction;
using System.Threading.Tasks;

namespace Mima.Infrastructure.Repositories.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            var res = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (!res.Succeeded)
            {
                return null;
            }
            return user; 
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<User> Register(string username, string email, string password)
        {
            var user = new User { UserName = username, Email = email };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return null;
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            await _userManager.AddToRoleAsync(user, "User");

            return user;
        }
    }
}
