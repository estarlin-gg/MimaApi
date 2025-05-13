using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mima.Application.Dtos;

namespace Mima.Application.Services.Abstraction
{
    public interface IAuthService
    {
        Task<AuthResponse> Register(RegisterDto userDto); 
        Task<AuthResponse> Login(LoginDto userDto);

        Task Logout();
    }
}
