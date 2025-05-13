using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mima.Application.Dtos
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
