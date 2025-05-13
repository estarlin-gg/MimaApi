using Microsoft.AspNetCore.Mvc;
using Mima.Application.Dtos;
using Mima.Application.Services.Abstraction;

namespace Mima.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto userDto)
        {
            var res = await _authService.Login(userDto);
            if (res == null) return Unauthorized("Credenciales incorrectas");
            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto userDto)
        {
            var res = await _authService.Register(userDto);
            if (res == null) return BadRequest("Error al crear cuenta");
            return Ok(res);
        }
    }
}
