using System;
using System.Security.Claims;
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
            var token = _service.Authenticate(new LoginRequestDto()
            {
                Username = dto.Username,
                Password = dto.Password
            });
            return Ok(new RegisterUserResponseDto
            {
                Id = createdUserId,
                Success = true,
                Token = token
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto dto)
        {
            var token = _service.Authenticate(dto);
            if (token == null)
                return Unauthorized();
            return Ok(new {token});
        }

        [HttpGet("user")]
        public IActionResult UserDetails()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            var user = _service.GetUserById(userId);
            return Ok(new UserDetailsResponseDto { Id = userId, Username = user.Username });
        }
    }
}