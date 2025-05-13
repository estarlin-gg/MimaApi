using Mima.Domain.Model;

namespace Mima.Infrastructure.Repositories.Abstraction
{
    public interface IAuthRepository
    {
        Task<User> Register(string username, string email, string passsword);
        Task<User> Login(string email, string password);
        Task Logout();
    }
}
