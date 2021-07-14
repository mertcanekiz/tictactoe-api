using System;
using System.Linq;
using System.Security.Claims;
using Core.Mongo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.DTO.Response;
using TicTacToe.Domain.Game;
using TicTacToe.Services;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IRepository<Game> _repository;
        private readonly IGameService _service;

        public GameController(IRepository<Game> repository, IGameService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_repository.FindAll().Select(x => new GameResponseDto(x)));
        }

        /// <summary>
        /// Create a new game
        /// </summary>
        /// <param name="dto">Game creation parameters</param>
        /// <returns>The id of the created game</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateGameResponseDto), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateGameRequestDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            var game = _service.CreateGame(dto, userId);
            return Ok(new CreateGameResponseDto {
                Game = new GameResponseDto(game),
                Success = true
            });
        }

        [HttpGet("{id}")]
        public IActionResult Show([FromRoute] Guid id)
        {
            var game = _repository.FindById(id);
            if (game == null)
                return NotFound(new {Id = id, Success = false});
            return Ok(new GameResponseDto(game));
        }

        [HttpPost("{id:guid}/makeMove")]
        public IActionResult MakeMove([FromRoute] Guid id, [FromBody] MakeMoveRequestDto move)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            var newState = _service.MakeMove(id, userId, move.X, move.Y);
            return Ok(newState);
        }

        [HttpPost("{id:guid}/join")]
        public IActionResult Join([FromRoute] Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            _service.JoinGame(id, userId);
            return Ok();
        }
    }
}