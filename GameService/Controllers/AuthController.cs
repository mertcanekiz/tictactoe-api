using Microsoft.AspNetCore.Mvc;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.DTO.Response;
using TicTacToe.Services;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto dto)
        {
            var createdUserId = _service.CreateUser(dto);
            return Ok(new RegisterUserResponseDto(createdUserId));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto dto)
        {
            var token = _service.Authenticate(dto);
            if (token == null)
                return Unauthorized();
            return Ok(new {token});
        }
    }
}